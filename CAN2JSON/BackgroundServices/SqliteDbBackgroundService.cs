using CAN2JSON.BMS;
using CAN2JSON.Data.Measurements;
using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Task = System.Threading.Tasks.Task;

namespace CAN2JSON.BackgroundServices;

public class SqliteDbBackgroundService : BackgroundService
{
    private readonly ApplicationInstance _application;
    private readonly ILogger<SqliteDbBackgroundService> _logger;
    private IServiceProvider Services { get; }
    
    public SqliteDbBackgroundService(IServiceProvider services, ILogger<SqliteDbBackgroundService> logger,
        ApplicationInstance application)
    {
        _logger = logger;
        _application = application;
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await PerformFunction(stoppingToken);

            await Task.Delay(2000, stoppingToken);
        }
    }

    private async Task PerformFunction(CancellationToken stoppingToken)
    {
        try
        {
            if (_application.Application["bms"] is BatteryManagementSystem bms)
            {
                var batteryReads = bms.Batteries.Select((battery, index) => new BatteryReading
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
                if (batteryReads.Any(br =>
                        br.DischargedTotal == 0 ||
                        br.ChargedTotal == 0 ||
                        br.TemperatureOne == 0 ||
                        br.TemperatureTwo == 0 ||
                        br.TemperatureMos == 0
                    )) return;

                if (bms.Voltage == 0) return;
                if (batteryReads.Count != 2) return;

                var bmsReads = new BmsReading()
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
                    FullChargedRestingVoltage = bms.FullChargedRestingVoltage,
                    BatteryReadings = batteryReads
                };

                //SQLite
                using var scope = Services.CreateScope();
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRepository<BmsReading>>();

                await scopedProcessingService.AddAsync(bmsReads);
            }

            if (_application.Application["rs485"] is List<BatteryCellMeasurement> cellReadings)
            {
                var batteryReads = cellReadings.Select((battery, index) => new BatteryCellReading
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
                        Date = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                        SlaveNumber = index
                    })
                    .ToList();
                
                if (batteryReads.Any(br =>
                        br.Cell01 == 0 ||
                        br.Cell05 == 0 ||
                        br.Cell09 == 0 ||
                        br.Cell13 == 0
                    )) return;
                
                //SQLite
                using var scope = Services.CreateScope();
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRepository<BatteryCellReading>>();
                
                foreach (BatteryCellReading reading in batteryReads)
                {
                    await scopedProcessingService.AddAsync(reading);
                }
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("{Exception}", exception.Message);
        }
    }
}