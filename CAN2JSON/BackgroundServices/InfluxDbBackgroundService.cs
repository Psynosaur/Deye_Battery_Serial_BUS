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
        var org = _configuration["InfluxDb:Org"];
        var url = _configuration["InfluxDb:Url"];
        if (bucket is null || org is null || url is null) throw new InvalidOperationException();
        var client =
            InfluxDBClientFactory.Create(url, _configuration["InfluxDb:Token"]?.ToCharArray());
        while (!stoppingToken.IsCancellationRequested)
        {
            await PerformFunction(bucket, client, org, stoppingToken);

            await Task.Delay(2000, stoppingToken);
        }
    }

    private Task PerformFunction(string bucket, InfluxDBClient client, string org, CancellationToken stoppingToken)
    {
        if (_application.Application["bms"] is BatteryManagementSystem bms)
        {
            var bmsInflux = new BmsMeasurement()
            {
                Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                ChargeCurrentLimit = bms.ChargeCurrentLimit,
                ChargeCurrentLimitMax = bms.ChargeCurrentLimitMax,
                Voltage = bms.Voltage,
                Amps = bms.Amps,
                Watts = bms.Watts,
                Temperature = bms.Temperature,
                StateOfCharge = bms.StateOfCharge,
                StateOfHealth = bms.StateOfHealth,
                CellVoltageHigh = bms.CellVoltageHigh,
                CellVoltageLow = bms.CellVoltageLow,
                CellVoltageDelta = bms.CellVoltageDelta,
                BmsTemperatureHigh = bms.BmsTemperatureHigh,
                BmsTemperatureLow = bms.BmsTemperatureLow,
                BatteryCapacity = bms.BatteryCapacity,
                ChargeVoltage = bms.ChargeVoltage,
                CurrentLimit = bms.CurrentLimit,
                DischargeLimit = bms.DischargeLimit,
                BatteryCutoffVoltage = bms.BatteryCutoffVoltage,
                FullChargedRestingVoltage = bms.FullChargedRestingVoltage
            };
            var battReadingsInflux =
                bms.Batteries.Select((battery, index) => new BatteryMeasurement()
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
                        Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
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

            if (bms.Voltage == 0) return Task.CompletedTask;
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

        return Task.CompletedTask;
    }
}