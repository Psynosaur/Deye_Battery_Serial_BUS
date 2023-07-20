using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Models;

public class BmsReading
{
    [Key] public int Id { get; set; }
    public int Date { get; set; }
    [Column(TypeName = "decimal(3, 0)")] public decimal ChargeCurrentLimit { get; set; }
    [Column(TypeName = "decimal(3, 0)")] public decimal ChargeCurrentLimitMax { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal Voltage { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal Amps { get; set; }
    [Column(TypeName = "decimal(5, 1)")] public decimal Watts { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal Temperature { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal StateOfCharge { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal StateOfHealth { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageHigh { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageLow { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageDelta { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BmsTemperatureHigh { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BmsTemperatureLow { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BatteryCapacity { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal ChargeVoltage { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal CurrentLimit { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal DischargeLimit { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BatteryCutoffVoltage { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal FullChargedRestingVoltage { get; set; }

    public List<BatteryReading>? BatteryReadings { get; set; }
}