using CAN2JSON.BMS;
using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;

namespace CAN2JSON.BackgroundServices;

public class DbBackgroundService : BackgroundService
{
    private readonly ApplicationInstance _application;
    private readonly ILogger<DbBackgroundService> _logger;
    public IServiceProvider Services { get; }
    public DbBackgroundService(IServiceProvider services,  ILogger<DbBackgroundService> logger, ApplicationInstance application)
    {
        _logger = logger;
        _application = application;
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Function executed at: {time}", DateTimeOffset.Now);
            // Call your function here
            await PerformFunction(stoppingToken);

            await Task.Delay(1000, stoppingToken); // Delay for 1 second
        }
    }

    private async Task PerformFunction(CancellationToken stoppingToken)
    {
        // Implement your function logic here
        Console.WriteLine("Performing function...");
        var bms = _application.Application["bms"] as BatteryManagementSystem;
        if (bms is not null)
        {
            var batteryReads = new List<BatteryReading>();
            foreach (var battery in bms.Batteries)
            {
                var bat = new BatteryReading
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
                    Cycles = battery.Cycles,
                    DateTime = DateTime.UtcNow.ToLocalTime(),
                };
                batteryReads.Add(bat);
            }
            if(bms.Voltage == 0) return;

            var bmsReads = new BmsReading()
            {
                DateTime = DateTime.UtcNow.ToLocalTime(),
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
            
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = 
                    scope.ServiceProvider
                        .GetRequiredService<IRepository<BmsReading>>();

                await scopedProcessingService.AddAsync(bmsReads);
            }
        }
    }
}