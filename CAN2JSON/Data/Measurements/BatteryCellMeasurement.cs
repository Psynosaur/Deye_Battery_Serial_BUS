using InfluxDB.Client.Core;
using RestSharp;

namespace CAN2JSON.Data.Measurements;
[Measurement("BatteryCellReading")]
public class BatteryCellMeasurement
{
    [Column(IsTimestamp = true)] public int Date { get; set; }
    [Column("BatteryNumber", IsTag = true)] public int SlaveNumber { get; set; }
    [Column("Cell01")] public decimal Cell01 { get; set; }
    [Column("Cell02")] public decimal Cell02 { get; set; }
    [Column("Cell03")] public decimal Cell03 { get; set; }
    [Column("Cell04")] public decimal Cell04 { get; set; }
    [Column("Cell05")] public decimal Cell05 { get; set; }
    [Column("Cell06")] public decimal Cell06 { get; set; }
    [Column("Cell07")] public decimal Cell07 { get; set; }
    [Column("Cell08")] public decimal Cell08 { get; set; }
    [Column("Cell09")] public decimal Cell09 { get; set; }
    [Column("Cell10")] public decimal Cell10 { get; set; }
    [Column("Cell11")] public decimal Cell11 { get; set; }
    [Column("Cell12")] public decimal Cell12 { get; set; }
    [Column("Cell13")] public decimal Cell13 { get; set; }
    [Column("Cell14")] public decimal Cell14 { get; set; }
    [Column("Cell15")] public decimal Cell15 { get; set; }
    [Column("Cell16")] public decimal Cell16 { get; set; }
    [Column("MinPos")] public decimal MinPos { get; set; }

    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["MinPos"] = MinPos;
        json["Cell01"] = Cell01;
        json["Cell02"] = Cell02;
        json["Cell03"] = Cell03;
        json["Cell04"] = Cell04;
        json["Cell05"] = Cell05;
        json["Cell06"] = Cell06;
        json["Cell07"] = Cell07;
        json["Cell08"] = Cell08;
        json["Cell09"] = Cell09;
        json["Cell10"] = Cell10;
        json["Cell11"] = Cell11;
        json["Cell12"] = Cell12;
        json["Cell13"] = Cell13;
        json["Cell14"] = Cell14;
        json["Cell15"] = Cell15;
        json["Cell16"] = Cell16;
        return json;
    }
}