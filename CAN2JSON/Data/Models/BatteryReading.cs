using System.ComponentModel.DataAnnotations;

namespace CAN2JSON.Data.Models;

public class BatteryReading
{
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int SlaveNumber { get; set; }
    public decimal BatteryVoltage { get; set; }
    public decimal BatteryCurrent { get; set; }
    public decimal StateOfCharge { get; set; }
    public decimal StateOfHealth { get; set; }

    public decimal CellVoltageHigh { get; set; }
    public decimal CellVoltageLow { get; set; }
        
    public decimal CellVoltageDelta { get; set; }
        
    public decimal TemperatureOne { get; set; }
    public decimal TemperatureTwo { get; set; }
    public decimal TemperatureMos { get; set; }

    public decimal CurrentLimit { get; set; }
    public decimal CurrentLimitMax { get; set; }

    public decimal ChargedTotal { get; set; }
    public decimal DischargedTotal { get; set; }
        
    public decimal Cycles { get; set; }
}