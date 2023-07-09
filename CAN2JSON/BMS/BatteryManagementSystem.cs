using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CAN2JSON.BMS;

public class BatteryManagementSystem
{
    public double ChargeVoltage { get; set; }
    public double CurrentLimit { get; set; }
    public double DischargeLimit { get; set; }
    public double BatteryCutoffVoltage { get; set; }
    public double ChargeCurrentLimit { get; set; }
    public double ChargeCurrentLimitMax { get; set; }
    public double CurrentStateVoltage { get; set; }
    public double CurrentStateCurrent { get; set; }
    public int CurrentStateTemperature { get; set; }
    public double CellVoltageHigh { get; set; }
    public double CellVoltageLow { get; set; }
    public int BmsTemperatureHigh { get; set; }
    public int BmsTemperatureLow { get; set; }
    public int BatteryCapacity { get; set; }
    public double FullChargedRestingVoltage { get; set; }
    public BmsStatuses Statuses { get; set; }
    
    public List<Battery> Batteries { get; set; }
    
    public List<CANFrame> CanFrames { get; set; }

    // Constructor
    public BatteryManagementSystem(int numBatteries)
    {
        Batteries = new List<Battery>(numBatteries);
        CanFrames = new List<CANFrame>();
        Statuses = new BmsStatuses();
    }

    // Methods
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
        json["ChargeVoltage"] = ChargeVoltage;
        json["CurrentLimit"] = CurrentLimit;
        json["DischargeLimit"] = DischargeLimit;
        json["BatteryCutoffVoltage"] = BatteryCutoffVoltage;
        json["ChargeCurrentLimit"] = ChargeCurrentLimit;
        json["ChargeCurrentLimitMax"] = ChargeCurrentLimitMax;
        json["CurrentStateVoltage"] = CurrentStateVoltage;
        json["CurrentStateCurrent"] = CurrentStateCurrent;
        json["CurrentStateTemperature"] = CurrentStateTemperature;
        json["CellVoltageHigh"] = CellVoltageHigh;
        json["CellVoltageLow"] = CellVoltageLow;
        json["BmsTemperatureHigh"] = BmsTemperatureHigh;
        json["BmsTemperatureLow"] = BmsTemperatureLow;
        json["BatteryCapacity"] = BatteryCapacity;
        json["FullChargedRestingVoltage"] = FullChargedRestingVoltage;
        json["Statuses"] = Statuses.ToJson();

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

        return json;
    }
}

public class BmsStatuses
{
    public byte BatteryCount { get; set; }
    public byte Status1 { get; set; }
    public byte Status2 { get; set; }
    public byte Status3 { get; set; }
    public byte Status4 { get; set; }
    public byte Status5 { get; set; }
    public byte Status6 { get; set; }
    public byte Status7 { get; set; }

    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["BatteryCount"] = BatteryCount;
        json["Status1"] = Status1;
        json["Status2"] = Status2;
        json["Status3"] = Status3;
        json["Status4"] = Status4;
        json["Status5"] = Status5;
        json["Status6"] = Status6;
        json["Status7"] = Status7;
        return json;
    }
}