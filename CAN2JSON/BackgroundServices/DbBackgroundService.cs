using CAN2JSON.BMS;
using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Task = System.Threading.Tasks.Task;

namespace CAN2JSON.BackgroundServices;

public class DbBackgroundService : BackgroundService
{
    private readonly ApplicationInstance _application;
    private readonly ILogger<DbBackgroundService> _logger;
    private IServiceProvider Services { get; }
    
    public DbBackgroundService(IServiceProvider services, ILogger<DbBackgroundService> logger,
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
        }
        catch (Exception exception)
        {
            _logger.LogError("{Exception}", exception.Message);
        }
    }
}