using System.Text.Json.Nodes;

namespace CAN2JSON.BMS
{
    public class Battery
    {
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
        public BatteryBmsStatuses Status { get; set; }

        public Battery()
        {
        }
        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json["Voltage"] = BatteryVoltage;
            json["Current"] = BatteryCurrent;
            json["StateOfCharge"] = StateOfCharge;
            json["StateOfHealth"] = StateOfHealth;
            json["CellHigh"] = CellVoltageHigh;
            json["CellLow"] = CellVoltageLow;
            json["CellDelta"] = CellVoltageDelta;
            json["Temp1"] = TemperatureOne;
            json["Temp2"] = TemperatureTwo;
            json["TemperatureMos"] = TemperatureMos;
            json["CurrentLimit"] = CurrentLimit;
            json["CurrentLimitMax"] = CurrentLimitMax;
            json["ChargedTotal"] = ChargedTotal;
            json["DischargedTotal"] = DischargedTotal;
            json["Cycles"] = Cycles;
            json["Status"] = Status.ToJson();
            return json;
        }
    }
}
public class BatteryBmsStatuses
{
    public byte Status1 { get; set; }
    public byte Status2 { get; set; }
    public byte Status3 { get; set; }
    public byte Status4 { get; set; }
    public byte Status5 { get; set; }
    public byte Status6 { get; set; }
    public byte Status7 { get; set; }
    public byte Status8 { get; set; }


    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["=>"] = $"0|1|2 {Status1}, {Status2}, cycles {Status3}, {Status4}, {Status5}, {Status6}, {Status7}, {Status8}";
        return json;
    }
}