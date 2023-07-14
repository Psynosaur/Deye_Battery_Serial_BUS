using System.ComponentModel.DataAnnotations;

namespace CAN2JSON.Data.Models;

public class BmsReading
{
    [Key]
    public int Id { get; set; }

    public DateTime DateTime { get; set; }
   
    public decimal ChargeCurrentLimit { get; set; }
    
    public decimal ChargeCurrentLimitMax { get; set; }
    
    public decimal Voltage { get; set; }
    
    public decimal Amps { get; set; }
    
    public decimal Watts { get; set; }
    
    public decimal Temperature { get; set; }
    
    public decimal StateOfCharge { get; set; }
    
    public decimal StateOfHealth { get; set; }
    
    public decimal CellVoltageHigh { get; set; }
    
    public decimal CellVoltageLow { get; set; }
    
    public decimal CellVoltageDelta { get; set; }
    
    public decimal BmsTemperatureHigh { get; set; }
    
    public decimal BmsTemperatureLow { get; set; }
    
    public decimal BatteryCapacity { get; set; }
    
    public decimal ChargeVoltage { get; set; }
    
    public decimal CurrentLimit { get; set; }
    
    public decimal DischargeLimit { get; set; }
    
    public decimal BatteryCutoffVoltage { get; set; }
    
    public decimal FullChargedRestingVoltage { get; set; }
    
    public List<BatteryReading>? BatteryReadings { get; set; }
}