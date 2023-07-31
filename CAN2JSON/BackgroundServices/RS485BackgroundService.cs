using System.IO.Ports;
using System.Text.Json;
using System.Text.Json.Nodes;
using CAN2JSON.Data.Measurements;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Task = System.Threading.Tasks.Task;

namespace CAN2JSON.BackgroundServices;

public class Rs485BackgroundService : IHostedService, IDisposable
{
    private readonly ILogger<Rs485BackgroundService> _logger;
    private readonly SerialPort _batteryOneSerial;
    private readonly SerialPort _batteryTwoSerial;
    private readonly JsonObject _document;
    private readonly ApplicationInstance _application;
    private readonly IConfiguration _configuration;
    private readonly byte[] _sendData = { 0xAA, 0x55, 0x01, 0x04, 0x00, 0x03, 0x70, 0x0D, 0x0A };
    private readonly CancellationTokenSource _cancellationTokenSource;

    public Rs485BackgroundService(ILogger<Rs485BackgroundService> logger, ApplicationInstance application,
        IConfiguration configuration)
    {
        _logger = logger;
        _application = application;
        _configuration = configuration;
        _document = new JsonObject();
        _batteryOneSerial = new SerialPort();
        _batteryTwoSerial = new SerialPort();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => BackgroundTask(_cancellationTokenSource.Token), cancellationToken);
        return Task.CompletedTask;
    }

    private async Task BackgroundTask(CancellationToken cancellationToken)
    {
        try
        {
            _application.Application["jsonSerial"] = _document;
            var batteryReadings = new List<BatteryCellMeasurement>(2);
            batteryReadings.Add(new BatteryCellMeasurement());
            batteryReadings.Add(new BatteryCellMeasurement());
            var bucket = _configuration["InfluxDb:CellVoltageBucket"];
            var org = _configuration["InfluxDb:Org"];
            var url = _configuration["InfluxDb:Url"];
            var batt1Port = _configuration["BatterySerial:Battery1"];
            var batt2Port = _configuration["BatterySerial:Battery2"];
            var interval = int.Parse(_configuration["BatterySerial:Interval"] ?? "5000");
            if (bucket is null || org is null || url is null || batt1Port is null || batt2Port is null)
                throw new InvalidOperationException();
            var client =
                InfluxDBClientFactory.Create(url, _configuration["InfluxDb:Token"]?.ToCharArray());

            _batteryOneSerial.BaudRate = 9600;
            _batteryTwoSerial.BaudRate = 9600;
            _batteryOneSerial.PortName = batt1Port;
            _batteryTwoSerial.PortName = batt2Port;
            _batteryOneSerial.ReadTimeout = 100;
            _batteryTwoSerial.ReadTimeout = 100;

            _batteryOneSerial.Open();
            _batteryTwoSerial.Open();

            while (!cancellationToken.IsCancellationRequested)
            {
                await SerialWriteReadMeasurement(cancellationToken, batteryReadings, client, bucket, org);
                // Wait for 2 seconds before sending data again
                await Task.Delay(interval, cancellationToken);
            }

            _batteryOneSerial.Close();
            _batteryTwoSerial.Close();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: " + ex.Message);
        }
    }

    private async Task SerialWriteReadMeasurement(CancellationToken cancellationToken,
        List<BatteryCellMeasurement> batteryReadings,
        InfluxDBClient client, string bucket, string org)
    {
        // Send data to the serial device
        _batteryOneSerial.Write(_sendData, 0, _sendData.Length);
        _batteryTwoSerial.Write(_sendData, 0, _sendData.Length);

        // Wait for a short time to ensure the data is sent
        await Task.Delay(150, cancellationToken);

        var received = await ReadSerialResponse(_batteryOneSerial);
        batteryReadings[0] = ProcessResponse(received, 0);

        received = await ReadSerialResponse(_batteryTwoSerial);
        batteryReadings[1] = ProcessResponse(received, 1);
        
        _application.Application["jsonSerial"] = ToJsonSerial(batteryReadings);

        // Add influx measurement
        using var writeApi = client.GetWriteApi();
        foreach (var batteryCellMeasurment in batteryReadings)
        {
            writeApi.WriteMeasurement(bucket, org,
                WritePrecision.Ns, batteryCellMeasurment);
        }
    }

    private async Task<byte[]> ReadSerialResponse(SerialPort serialPort)
    {
        // Read the response from the serial device
        byte[] buffer = new byte[28 * 3];
        int bytesRead = await serialPort.BaseStream.ReadAsync(buffer, 0, buffer.Length);
        byte[] received = new byte[bytesRead];
        Buffer.BlockCopy(buffer, 0, received, 0, bytesRead);

        _logger.LogInformation("{Date}: {SerialDevice}: {BytesRead} bytes: {Replace}",
            DateTime.UtcNow.ToLocalTime(), serialPort.PortName, bytesRead,
            BitConverter.ToString(received).Replace("-", " "));
        return received;
    }

    public JsonObject ToJsonSerial(List<BatteryCellMeasurement> batteryCellMeasurments)
    {
        var json = new JsonObject();
        var serialArray = new JsonArray();
        foreach (var batteryCell in batteryCellMeasurments)
        {
            using var document = JsonDocument.Parse(batteryCell.ToJson().ToString() ?? string.Empty);
            var frameJsonObject = document.RootElement.Clone();
            serialArray.Add(frameJsonObject);
        }

        json["BatteryCells"] = serialArray;
        return json;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        _batteryOneSerial.Close();
        _batteryTwoSerial.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _batteryOneSerial?.Dispose();
        _batteryTwoSerial?.Dispose();
        _cancellationTokenSource?.Dispose();
    }

    public BatteryCellMeasurement ProcessResponse(byte[] bytes, int slaveNumber)
    {
        var dataOffset = 12;
        var cellMeasurment = new BatteryCellMeasurement();
        if (bytes.Length == 81)
        {
            cellMeasurment.MinPos = BitConverter.ToInt16(new[] { bytes[dataOffset - 2], bytes[dataOffset - 1] });
            cellMeasurment.Cell01 = BitConverter.ToInt16(new[] { bytes[dataOffset + 2], bytes[dataOffset + 3] });
            cellMeasurment.Cell02 = BitConverter.ToInt16(new[] { bytes[dataOffset + 4], bytes[dataOffset + 5] });
            cellMeasurment.Cell03 = BitConverter.ToInt16(new[] { bytes[dataOffset + 6], bytes[dataOffset + 7] });
            cellMeasurment.Cell04 = BitConverter.ToInt16(new[] { bytes[dataOffset + 8], bytes[dataOffset + 9] });
            cellMeasurment.Cell05 = BitConverter.ToInt16(new[] { bytes[dataOffset + 10], bytes[dataOffset + 11] });
            cellMeasurment.Cell06 = BitConverter.ToInt16(new[] { bytes[dataOffset + 12], bytes[dataOffset + 13] });
            cellMeasurment.Cell07 = BitConverter.ToInt16(new[] { bytes[dataOffset + 14], bytes[dataOffset + 15] });
            cellMeasurment.Cell08 = BitConverter.ToInt16(new[] { bytes[dataOffset + 16], bytes[dataOffset + 17] });
            cellMeasurment.Cell09 = BitConverter.ToInt16(new[] { bytes[dataOffset + 18], bytes[dataOffset + 19] });
            cellMeasurment.Cell10 = BitConverter.ToInt16(new[] { bytes[dataOffset + 20], bytes[dataOffset + 21] });
            cellMeasurment.Cell11 = BitConverter.ToInt16(new[] { bytes[dataOffset + 22], bytes[dataOffset + 23] });
            cellMeasurment.Cell12 = BitConverter.ToInt16(new[] { bytes[dataOffset + 24], bytes[dataOffset + 25] });
            cellMeasurment.Cell13 = BitConverter.ToInt16(new[] { bytes[dataOffset + 26], bytes[dataOffset + 27] });
            cellMeasurment.Cell14 = BitConverter.ToInt16(new[] { bytes[dataOffset + 28], bytes[dataOffset + 29] });
            cellMeasurment.Cell15 = BitConverter.ToInt16(new[] { bytes[dataOffset + 30], bytes[dataOffset + 31] });
            // This one has bytes at the end and front of the data array, rs485 manufacturer tool verified this
            cellMeasurment.Cell16 = BitConverter.ToInt16(new[] { bytes[dataOffset + 32], bytes[dataOffset + 1] });
            cellMeasurment.Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            cellMeasurment.SlaveNumber = slaveNumber;
        }

        return cellMeasurment;
    }
}