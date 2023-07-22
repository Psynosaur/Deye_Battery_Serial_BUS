using System.ComponentModel.DataAnnotations;
using InfluxDB.Client.Core;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Models;
[Measurement("BatteryReading")]
public class BatteryReadingInFlux
{
    [Key] public int Id { get; set; }
    [Column(IsTimestamp = true)] public int Date { get; set; }
    [Column("SlaveNumber", IsTag = true)] public int SlaveNumber { get; set; }
    [Column("BatteryVoltage")] public decimal BatteryVoltage { get; set; }
    [Column("BatteryCurrent")] public decimal BatteryCurrent { get; set; }
    [Column("StateOfCharge")] public decimal StateOfCharge { get; set; }
    [Column("StateOfHealth")] public decimal StateOfHealth { get; set; }
    [Column("CellVoltageHigh")] public decimal CellVoltageHigh { get; set; }
    [Column("CellVoltageLow")] public decimal CellVoltageLow { get; set; }
    [Column("CellVoltageDelta")] public decimal CellVoltageDelta { get; set; }
    [Column("TemperatureOne")] public decimal TemperatureOne { get; set; }
    [Column("TemperatureTwo")] public decimal TemperatureTwo { get; set; }
    [Column("TemperatureMos")] public decimal TemperatureMos { get; set; }
    [Column("CurrentLimit")] public decimal CurrentLimit { get; set; }
    [Column("CurrentLimitMax")] public decimal CurrentLimitMax { get; set; }
    [Column("ChargedTotal")] public decimal ChargedTotal { get; set; }
    [Column("DischargedTotal")] public decimal DischargedTotal { get; set; }
    public decimal Cycles { get; set; }
}