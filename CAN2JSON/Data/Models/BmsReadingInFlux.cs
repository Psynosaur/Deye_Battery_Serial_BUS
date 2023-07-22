using System.ComponentModel.DataAnnotations;
using InfluxDB.Client.Core;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Models;
[Measurement("BmsReading")]
public class BmsReadingInflux
{
    [Key] public int Id { get; set; }
    [Column(IsTimestamp = true)] public int Date { get; set; }
    [Column("ChargeCurrentLimit")] public decimal ChargeCurrentLimit { get; set; }
    [Column("ChargeCurrentLimitMax")] public decimal ChargeCurrentLimitMax { get; set; }
    [Column("Voltage")] public decimal Voltage { get; set; }
    [Column("Amps")] public decimal Amps { get; set; }
    [Column("Watts")] public decimal Watts { get; set; }
    [Column("Temperature")] public decimal Temperature { get; set; }
    [Column("StateOfCharge")] public decimal StateOfCharge { get; set; }
    [Column("StateOfHealth")] public decimal StateOfHealth { get; set; }
    [Column("CellVoltageHigh")] public decimal CellVoltageHigh { get; set; }
    [Column("CellVoltageLow")] public decimal CellVoltageLow { get; set; }
    [Column("CellVoltageDelta")] public decimal CellVoltageDelta { get; set; }
    [Column("BmsTemperatureHigh")] public decimal BmsTemperatureHigh { get; set; }
    [Column("BmsTemperatureLow")] public decimal BmsTemperatureLow { get; set; }
    [Column("BatteryCapacity")] public decimal BatteryCapacity { get; set; }
    [Column("ChargeVoltage")] public decimal ChargeVoltage { get; set; }
    [Column("CurrentLimit")] public decimal CurrentLimit { get; set; }
    [Column("DischargeLimit")] public decimal DischargeLimit { get; set; }
    [Column("BatteryCutoffVoltage")] public decimal BatteryCutoffVoltage { get; set; }
    [Column("FullChargedRestingVoltage")] public decimal FullChargedRestingVoltage { get; set; }
}