using System.Text.Json.Nodes;

namespace CAN2JSON.BMS
{
    public class Battery
    {
        public double BatteryVoltage { get; set; }
        public double BatteryCurrent { get; set; }
        public double StateOfCharge { get; set; }
        public double StateOfHealth { get; set; }

        public double CellVoltageHigh { get; set; }
        public double CellVoltageLow { get; set; }
        
        public double CellVoltageDelta { get; set; }
        
        public int TemperatureOne { get; set; }
        public int TemperatureTwo { get; set; }
        public int TemperatureMos { get; set; }

        public double CurrentLimit { get; set; }
        public double CurrentLimitMax { get; set; }

        public double ChargedTotal { get; set; }
        public double DischargedTotal { get; set; }

        public Battery()
        {
        }
        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json["BatteryVoltage"] = BatteryVoltage;
            json["BatteryCurrent"] = BatteryCurrent;
            json["StateOfCharge"] = StateOfCharge;
            json["StateOfHealth"] = StateOfHealth;
            json["CellVoltageHigh"] = CellVoltageHigh;
            json["CellVoltageLow"] = CellVoltageLow;
            json["CellVoltageDelta"] = CellVoltageDelta;
            json["TemperatureOne"] = TemperatureOne;
            json["TemperatureTwo"] = TemperatureTwo;
            json["TemperatureMos"] = TemperatureMos;
            json["CurrentLimit"] = CurrentLimit;
            json["CurrentLimitMax"] = CurrentLimitMax;
            json["ChargedTotal"] = ChargedTotal;
            json["DischargedTotal"] = DischargedTotal;
            return json;
        }
    }
}