using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Models;

public class BatteryReading
{
    [Key] public int Id { get; set; }
    public DateTime DateTime { get; set; }

    public int SlaveNumber { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BatteryVoltage { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal BatteryCurrent { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal StateOfCharge { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal StateOfHealth { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageHigh { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageLow { get; set; }
    [Column(TypeName = "decimal(4, 3)")] public decimal CellVoltageDelta { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal TemperatureOne { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal TemperatureTwo { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal TemperatureMos { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal CurrentLimit { get; set; }
    [Column(TypeName = "decimal(3, 1)")] public decimal CurrentLimitMax { get; set; }

    [Column(TypeName = "decimal(7, 3)")] public decimal ChargedTotal { get; set; }
    [Column(TypeName = "decimal(7, 3)")] public decimal DischargedTotal { get; set; }

    [Column(TypeName = "decimal(4, 3)")] public decimal Cycles { get; set; }
}