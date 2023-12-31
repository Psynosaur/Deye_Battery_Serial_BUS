using System.Text.Json;
using System.Text.Json.Nodes;

namespace CAN2JSON.BMS;

public class BatteryManagementSystem
{
    public decimal ChargeVoltage { get; set; }
    public decimal CurrentLimit { get; set; }
    public decimal DischargeLimit { get; set; }
    public decimal BatteryCutoffVoltage { get; set; }
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
    
    public decimal FullChargedRestingVoltage { get; set; }
    public string? LastUpdate { get; set; }
    public BmsStatuses? Statuses { get; set; }

    public List<Battery> Batteries { get; set; }

    public List<CANFrame> CanFrames { get; set; }
    
    public BatteryManagementSystem(int numBatteries)
    {
        Batteries = new List<Battery>(numBatteries);
        CanFrames = new List<CANFrame>();
        Statuses = new BmsStatuses();
    }

    public void UpdateBattery(int batteryIndex, Battery battery)
    {
        CheckBatteryIndex(batteryIndex);
        Batteries[batteryIndex] = battery;
    }

    private void CheckBatteryIndex(int batteryIndex)
    {
        if (batteryIndex < 0 || batteryIndex >= Batteries.Count)
            throw new ArgumentOutOfRangeException(nameof(batteryIndex), "Invalid battery index.");
    }
    
    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["SOC"] = StateOfCharge;
        json["SOH"] = StateOfHealth;
        json["Voltage"] = Voltage;
        json["Amp"] = Amps;
        json["Watts"] = Watts;
        json["Temp"] = Temperature;
        json["Cell(V) H"] = CellVoltageHigh;
        json["Cell(V) L"] = CellVoltageLow;
        json["Cell(V) d"] = CellVoltageHigh - CellVoltageLow /1m;
        json["BmsTempHigh"] = BmsTemperatureHigh;
        json["BmsTempLow"] = BmsTemperatureLow;
        json["ChargeLimit(A)"] = ChargeCurrentLimit;
        json["ChargeLimitMax(A)"] = ChargeCurrentLimitMax;
        json["Cut off(V)"] = BatteryCutoffVoltage;
        json["Charge(V)"] = ChargeVoltage;
        json["CurrentLimit(A)"] = CurrentLimit;
        json["DischargeLimit(A)"] = DischargeLimit;
        json["BatteryCapacity(Ah)"] = BatteryCapacity;
        json["Resting(V)"] = FullChargedRestingVoltage;
        json["Statuses"] = Statuses is not null ? Statuses.ToJson() : "";
        var batteriesJsonArray = new JsonArray();
        foreach (var battery in Batteries)
        {
            var batteryJsonString = battery.ToJson().ToString();
            using var document = JsonDocument.Parse(batteryJsonString);
            var batteryJsonObject = document.RootElement.Clone();
            batteriesJsonArray.Add(batteryJsonObject);
        }
        json["Batteries"] = batteriesJsonArray;

        var canFramesJsonArray = new JsonArray();
        foreach (var frame in CanFrames)
        {
            var frameJsonString = frame.ToJson().ToString();
            using var document = JsonDocument.Parse(frameJsonString);
            var frameJsonObject = document.RootElement.Clone();
            canFramesJsonArray.Add(frameJsonObject);
        }
        json["CanFrames"] = canFramesJsonArray;
        json["date"] = LastUpdate;
        
        return json;
    }
}

public class BmsStatuses
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
        json["=>"] = $"{Status1}, {Status2}, {Status3}, {Status4}, {Status5}, {Status6}, {Status7}, {Status8}";
        return json;
    }
}