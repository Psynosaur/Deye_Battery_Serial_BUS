using System.IO.Ports;
using System.Text.Json;
using System.Text.Json.Nodes;
using CAN2JSON.Data.Measurements;
using Task = System.Threading.Tasks.Task;

namespace CAN2JSON.BackgroundServices;

public class Rs485BackgroundService : BackgroundService
{
    private readonly ILogger<Rs485BackgroundService> _logger;
    private readonly SerialPort _batteryOneSerial;
    private readonly SerialPort _batteryTwoSerial;
    private readonly JsonObject _document;
    private readonly ApplicationInstance _application;
    private readonly IConfiguration _configuration;
    private readonly byte[] _sendData = { 0xAA, 0x55, 0x01, 0x04, 0x00, 0x03, 0x70, 0x0D, 0x0A };

    public Rs485BackgroundService(ILogger<Rs485BackgroundService> logger, ApplicationInstance application,
        IConfiguration configuration)
    {
        _logger = logger;
        _application = application;
        _configuration = configuration;
        _document = new JsonObject();
        _batteryOneSerial = new SerialPort();
        _batteryTwoSerial = new SerialPort();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _application.Application["jsonSerial"] = _document;
        var batteryReadings = new List<BatteryCellMeasurement>(2);
        batteryReadings.Add(new BatteryCellMeasurement());
        batteryReadings.Add(new BatteryCellMeasurement());
        var batt1Port = _configuration["BatterySerial:Battery1"];
        var batt2Port = _configuration["BatterySerial:Battery2"];
        var interval = int.Parse(_configuration["BatterySerial:Interval"] ?? "5000");
        if (batt1Port is null || batt2Port is null)
            throw new InvalidOperationException();

        _batteryOneSerial.BaudRate = 9600;
        _batteryTwoSerial.BaudRate = 9600;
        _batteryOneSerial.PortName = batt1Port;
        _batteryTwoSerial.PortName = batt2Port;
        _batteryOneSerial.ReadTimeout = 100;
        _batteryTwoSerial.ReadTimeout = 100;

        _batteryOneSerial.Open();
        _batteryTwoSerial.Open();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await SerialWriteReadMeasurement(stoppingToken, batteryReadings);

            await Task.Delay(interval, stoppingToken);
        }
        
        _batteryOneSerial.Close();
        _batteryTwoSerial.Close();
    }
    
    private async Task SerialWriteReadMeasurement(CancellationToken cancellationToken,
        List<BatteryCellMeasurement> batteryReadings)
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

        // Controller output
        _application.Application["jsonSerial"] = ToJsonSerial(batteryReadings);
        // BatteryCellReading list, for db workers to map and store 
        _application.Application["rs485"] = batteryReadings;
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
    
    public BatteryCellMeasurement ProcessResponse(byte[] bytes, int slaveNumber)
    {
        var dataOffset = 13;
        var cellMeasurment = new BatteryCellMeasurement();
        if (bytes.Length == 81)
        {
            cellMeasurment.MinPos = BitConverter.ToInt16(new[] { bytes[dataOffset - 3], bytes[dataOffset - 2] });
            cellMeasurment.Cell01 = BitConverter.ToInt16(new[] { bytes[dataOffset + 1], bytes[dataOffset] });
            cellMeasurment.Cell02 = BitConverter.ToInt16(new[] { bytes[dataOffset + 3], bytes[dataOffset + 2] });
            cellMeasurment.Cell03 = BitConverter.ToInt16(new[] { bytes[dataOffset + 5], bytes[dataOffset + 4] });
            cellMeasurment.Cell04 = BitConverter.ToInt16(new[] { bytes[dataOffset + 7], bytes[dataOffset + 6] });
            cellMeasurment.Cell05 = BitConverter.ToInt16(new[] { bytes[dataOffset + 9], bytes[dataOffset + 8] });
            cellMeasurment.Cell06 = BitConverter.ToInt16(new[] { bytes[dataOffset + 11], bytes[dataOffset + 10] });
            cellMeasurment.Cell07 = BitConverter.ToInt16(new[] { bytes[dataOffset + 13], bytes[dataOffset + 12] });
            cellMeasurment.Cell08 = BitConverter.ToInt16(new[] { bytes[dataOffset + 15], bytes[dataOffset + 14] });
            cellMeasurment.Cell09 = BitConverter.ToInt16(new[] { bytes[dataOffset + 17], bytes[dataOffset + 16] });
            cellMeasurment.Cell10 = BitConverter.ToInt16(new[] { bytes[dataOffset + 19], bytes[dataOffset + 18] });
            cellMeasurment.Cell11 = BitConverter.ToInt16(new[] { bytes[dataOffset + 21], bytes[dataOffset + 20] });
            cellMeasurment.Cell12 = BitConverter.ToInt16(new[] { bytes[dataOffset + 23], bytes[dataOffset + 22] });
            cellMeasurment.Cell13 = BitConverter.ToInt16(new[] { bytes[dataOffset + 25], bytes[dataOffset + 24] });
            cellMeasurment.Cell14 = BitConverter.ToInt16(new[] { bytes[dataOffset + 27], bytes[dataOffset + 26] });
            cellMeasurment.Cell15 = BitConverter.ToInt16(new[] { bytes[dataOffset + 29], bytes[dataOffset + 28] });
            cellMeasurment.Cell16 = BitConverter.ToInt16(new[] { bytes[dataOffset + 31], bytes[dataOffset + 30] });
            cellMeasurment.Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            cellMeasurment.SlaveNumber = slaveNumber;
        }

        return cellMeasurment;
    }
}