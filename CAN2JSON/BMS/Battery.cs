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
        public BmsStatuses Status { get; set; }

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