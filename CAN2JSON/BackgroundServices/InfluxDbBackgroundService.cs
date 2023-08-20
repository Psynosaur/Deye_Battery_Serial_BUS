using CAN2JSON.BMS;
using CAN2JSON.Data.Measurements;
using CAN2JSON.Data.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Task = System.Threading.Tasks.Task;

namespace CAN2JSON.BackgroundServices;

public class InfluxDbBackgroundService : BackgroundService
{
    private readonly ApplicationInstance _application;
    private readonly ILogger<InfluxDbBackgroundService> _logger;
    private readonly IConfiguration _configuration;

    public InfluxDbBackgroundService(ILogger<InfluxDbBackgroundService> logger,
        ApplicationInstance application, IConfiguration configuration)
    {
        _logger = logger;
        _application = application;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bucket = _configuration["InfluxDb:Bucket"];
        var cellBucket = _configuration["InfluxDb:CellVoltageBucket"];
        var org = _configuration["InfluxDb:Org"];
        var url = _configuration["InfluxDb:Url"];
        var interval = int.Parse(_configuration["BatterySerial:Interval"] ?? "2000");
        if (bucket is null || org is null || url is null || cellBucket is null) throw new ArgumentNullException();
        var client =
            InfluxDBClientFactory.Create(url, _configuration["InfluxDb:Token"]?.ToCharArray());
        while (!stoppingToken.IsCancellationRequested)
        {
            await PerformFunction(bucket, cellBucket, client, org, stoppingToken);

            await Task.Delay(interval, stoppingToken);
        }
    }

    private Task PerformFunction(string bucket, string cellBucket, InfluxDBClient client, string org, CancellationToken stoppingToken)
    {
        if (_application.Application["bms"] is BatteryManagementSystem bms)
        {
            var tbms = bms;
            var bmsInflux = new BmsMeasurement()
            {
                Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                ChargeCurrentLimit = tbms.ChargeCurrentLimit,
                ChargeCurrentLimitMax = tbms.ChargeCurrentLimitMax,
                Voltage = tbms.Voltage,
                Amps = tbms.Amps,
                Watts = tbms.Watts,
                Temperature = tbms.Temperature,
                StateOfCharge = tbms.StateOfCharge,
                StateOfHealth = tbms.StateOfHealth,
                CellVoltageHigh = tbms.CellVoltageHigh,
                CellVoltageLow = tbms.CellVoltageLow,
                CellVoltageDelta = tbms.CellVoltageDelta,
                BmsTemperatureHigh = tbms.BmsTemperatureHigh,
                BmsTemperatureLow = tbms.BmsTemperatureLow,
                BatteryCapacity = tbms.BatteryCapacity,
                ChargeVoltage = tbms.ChargeVoltage,
                CurrentLimit = tbms.CurrentLimit,
                DischargeLimit = tbms.DischargeLimit,
                BatteryCutoffVoltage = tbms.BatteryCutoffVoltage,
                FullChargedRestingVoltage = tbms.FullChargedRestingVoltage
            };
            var battReadingsInflux =
                tbms.Batteries.Select((battery, index) => new BatteryMeasurement()
                    {
                        BatteryVoltage = battery.BatteryVoltage,
                        BatteryCurrent = battery.BatteryCurrent,
                        StateOfCharge = battery.StateOfCharge,
                        StateOfHealth = battery.StateOfHealth,
                        CellVoltageHigh = battery.CellVoltageHigh,
                        CellVoltageLow = battery.CellVoltageLow,
                        CellVoltageDelta = battery.CellVoltageDelta,
                        TemperatureOne = battery.TemperatureOne,
                        TemperatureTwo = battery.TemperatureTwo,
                        TemperatureMos = battery.TemperatureMos,
                        CurrentLimit = battery.CurrentLimit,
                        CurrentLimitMax = battery.CurrentLimitMax,
                        ChargedTotal = battery.ChargedTotal,
                        DischargedTotal = battery.DischargedTotal,
                        Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                        SlaveNumber = index
                    })
                    .ToList();

            if (battReadingsInflux.Any(br =>
                    br.DischargedTotal == 0 ||
                    br.ChargedTotal == 0 ||
                    br.TemperatureOne == 0 ||
                    br.TemperatureTwo == 0 ||
                    br.TemperatureMos == 0
                )) return Task.CompletedTask;

            if (tbms.Voltage == 0) return Task.CompletedTask;
            if (battReadingsInflux.Count != 2) return Task.CompletedTask;

            // Influx DB
            using var writeApi = client.GetWriteApi();
            writeApi.WriteMeasurement(bucket, _configuration["InfluxDb:Org"],
                WritePrecision.Ns, bmsInflux);
            // Add battery measurements 
            for (var index = 0; index < battReadingsInflux.Count; index++)
            {
                var batteryReading = battReadingsInflux[index];
                writeApi.WriteMeasurement(_configuration["InfluxDb:Bucket"], _configuration["InfluxDb:Org"],
                    WritePrecision.Ns, batteryReading);
            }
        }

        if (_application.Application["rs485"] is List<BatteryCellMeasurement> bcm)
        {
            var bcms = bcm;
            var batteryCellMeasurements = bcms.Select((battery, index) => new BatteryCellMeasurement()
                {
                    Cell01 = battery.Cell01,
                    Cell02 = battery.Cell02,
                    Cell03 = battery.Cell03,
                    Cell04 = battery.Cell04,
                    Cell05 = battery.Cell05,
                    Cell06 = battery.Cell06,
                    Cell07 = battery.Cell07,
                    Cell08 = battery.Cell08,
                    Cell09 = battery.Cell09,
                    Cell10 = battery.Cell10,
                    Cell11 = battery.Cell11,
                    Cell12 = battery.Cell12,
                    Cell13 = battery.Cell13,
                    Cell14 = battery.Cell14,
                    Cell15 = battery.Cell15,
                    Cell16 = battery.Cell16,
                    MinPos = battery.MinPos,
                    Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                    SlaveNumber = index
                })
                .ToList();
            if (batteryCellMeasurements.Any(br =>
                    br.Cell01 == 0 ||
                    br.Cell05 == 0 ||
                    br.Cell09 == 0 ||
                    br.Cell13 == 0
                )) return Task.CompletedTask;
            
            // Add influx measurement
            using var writeApi = client.GetWriteApi();
            foreach (var batteryCellMeasurment in batteryCellMeasurements)
            {
                writeApi.WriteMeasurement(cellBucket, org,
                    WritePrecision.Ns, batteryCellMeasurment);
            }
        }

        return Task.CompletedTask;
    }
}