﻿using System.Buffers.Binary;
using System.Globalization;
using System.IO.Ports;
using System.Text.Json.Nodes;
using CAN2JSON.BMS;
using HidSharp;

namespace CAN2JSON.BackgroundServices;

public class SerialDataBackgroundService : BackgroundService
{
    private readonly ILogger<SerialDataBackgroundService> _logger;
    private readonly SerialPort _serialPort;
    private readonly JsonObject _document;
    private readonly ApplicationInstance _application;
    private readonly BatteryManagementSystem _batteryManagementSystem = new(1);
    private bool _firstFrame = true;
    private bool _batteryCountSet;
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Indicates whether the current application is running on Linux.
    /// </summary>
    public static bool IsLinux() =>
        #if TARGET_LINUX && !TARGET_ANDROID
                    true;
        #else
                false;
        #endif

    public SerialDataBackgroundService(ILogger<SerialDataBackgroundService> logger, ApplicationInstance application, IConfiguration configuration)
    {
        _logger = logger;
        _application = application;
        _configuration = configuration;
        _document = new JsonObject();
        _serialPort = new SerialPort(DetermineSerialDeviceName(), 2000000);
    }

    public string DetermineSerialDeviceName()
    {
        var list = DeviceList.Local;
        var allDeviceList = list.GetAllDevices().ToArray();
        string devicePath = _configuration["CANDevice:Path"] ?? "/dev/ttyUSB0";
        foreach (Device? device in allDeviceList)
        {
            if (!IsLinux())
            {
                if (device.GetFriendlyName().Equals($"USB-SERIAL CH340 ({device.GetFileSystemName()})"))
                {
                    devicePath = device.GetFileSystemName();
                    break;
                }
            }
            Console.WriteLine($"File System: {device.GetFileSystemName()}\nFriendly: {device.GetFriendlyName()}\n");
        }

        // if (!IsLinux()) if (devicePath.Equals("/dev/ttyUSB0")) throw new InvalidOperationException(message:"No compatible serial devices found");
        return devicePath;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _application.Application["json"] = _document;
            _application.Application["bms"] = _batteryManagementSystem;
           
            
            _serialPort.Open();
            _serialPort.DataReceived += SerialPort_DataReceived;

            while (!stoppingToken.IsCancellationRequested)
            {
                // Process serial data continuously
                await Task.Delay(100, stoppingToken); // Adjust the delay as needed
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing serial data: ${Ex}", ex);
        }
        finally
        {
            _serialPort.DataReceived -= SerialPort_DataReceived;
            _serialPort.Close();
            _serialPort.Dispose();
        }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        int bytes = sp.BytesToRead;
        byte[] buffer = new byte[bytes];
        sp.Read(buffer, 0, bytes);
        if (bytes is not 20) return;
        List<byte> littleEndianBytes = new List<byte>(bytes);
        littleEndianBytes.AddRange(buffer.Select(BinaryPrimitives.ReverseEndianness));
        try
        {
            ProcessReceivedData(littleEndianBytes.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in SerialPort_DataReceived: ${Ex}", ex);
        }
    }

    private void ProcessReceivedData(byte[] receivedData)
    {
        // Parse the received data and update the corresponding frame values
        /*
         *  ** ** ** ** ** ID ID ** ** DS DB DB DB DB DB DB DB DB ** ??
         *  0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19
         *  -----------------------------------------------------------
         *  AA-55-01-01-01-56-03-00-00-08-96-14-8B-00-82-00-00-00-00-1B
        */

        if (receivedData.Length is not 20) return;
        var id = $"{BitConverter.ToString(new[] { receivedData[6], receivedData[5] }).Replace("-", string.Empty)}";
        try
        {
            var canFrame = new CANFrame(id);
            canFrame.UpdateValues(receivedData);
            UpdateCanFrames(canFrame);
            _batteryManagementSystem.LastUpdate = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            
            _application.Application["json"] = _batteryManagementSystem.ToJson();
            _application.Application["bms"] = _batteryManagementSystem;
        }
        catch (Exception ex)
        {
            var errFrameData = BitConverter.ToString(receivedData);
            _logger.LogError(ex, errFrameData);
        }
    }

    private void UpdateCanFrames(CANFrame canFrame)
    {
        if (_firstFrame)
        {
            _batteryManagementSystem.CanFrames.Add(canFrame);
            // _batteryManagementSystem.XmlTemplate = File.ReadAllText(@"CANInformation\Deye slim.xml");
            _firstFrame = false;
        }

        StatusUpdateAndAddBatteries(canFrame);

        UpdateBatteryInformation(canFrame);

        UpdateBmsData(canFrame);

        if (!_batteryManagementSystem.CanFrames.Any(cf => cf.FrameId.Equals(canFrame.FrameId)))
        {
            _batteryManagementSystem.CanFrames.Add(canFrame);
        }

        var copyArr = _batteryManagementSystem.CanFrames;
        for (var index = 0; index < copyArr.Count; index++)
        {
            if (copyArr[index].FrameId == canFrame.FrameId) copyArr[index] = canFrame;
        }

        _batteryManagementSystem.CanFrames = copyArr;
    }

    private void UpdateBatteryInformation(CANFrame canFrame)
    {
        for (int i = 0; i < _batteryManagementSystem.Batteries.Count; i++)
        {
            if (canFrame.FrameId.Equals($"015{i}"))
            {
                _batteryManagementSystem.Batteries[i].BatteryVoltage = canFrame.DataInShorts[0] / 10m;
                _batteryManagementSystem.Batteries[i].BatteryCurrent = canFrame.DataInShorts[1] / -10m;
                _batteryManagementSystem.Batteries[i].StateOfCharge = canFrame.DataInShorts[2] / 10m;
                _batteryManagementSystem.Batteries[i].StateOfHealth = canFrame.DataInShorts[3] / 10m;
                continue;
            }

            if (canFrame.FrameId.Equals($"020{i}"))
            {
                _batteryManagementSystem.Batteries[i].CellVoltageHigh = canFrame.DataInShorts[0] / 1000m;
                _batteryManagementSystem.Batteries[i].CellVoltageLow = canFrame.DataInShorts[1] / 1000m;
                _batteryManagementSystem.Batteries[i].TemperatureOne = canFrame.DataInShorts[2] / 10m;
                _batteryManagementSystem.Batteries[i].TemperatureTwo = canFrame.DataInShorts[3] / 10m;
                _batteryManagementSystem.Batteries[i].CellVoltageDelta =
                    canFrame.DataInShorts[0] / 1000m - canFrame.DataInShorts[1] / 1000m;

                continue;
            }

            if (canFrame.FrameId.Equals($"025{i}"))
            {
                _batteryManagementSystem.Batteries[i].TemperatureMos = canFrame.DataInShorts[0] / 10m;
                // _batteryManagementSystem.Batteries[i].CurrentLimit = canFrame.DataInShorts[1]/10m;
                _batteryManagementSystem.Batteries[i].CurrentLimit = canFrame.DataInShorts[2] / 1m;
                _batteryManagementSystem.Batteries[i].CurrentLimitMax = canFrame.DataInShorts[3] / 1m;
                continue;
            }

            if (canFrame.FrameId.Equals($"040{i}"))
            {
                _batteryManagementSystem.Batteries[i].Status = new BatteryBmsStatuses
                {
                    Status1 = canFrame.Data[10],
                    Status2 = canFrame.Data[11],
                    Status3 = canFrame.Data[12],
                    Status4 = canFrame.Data[13],
                    Status5 = canFrame.Data[14],
                    Status6 = canFrame.Data[15],
                    Status7 = canFrame.Data[16],
                    Status8 = canFrame.Data[17]
                };
                continue;
            }

            if (canFrame.FrameId.Equals($"055{i}"))
            {
                _batteryManagementSystem.Batteries[i].ChargedTotal = canFrame.DataInInt32[0] / 1000m;
                _batteryManagementSystem.Batteries[i].DischargedTotal = canFrame.DataInInt32[1] / 1000m;
                _batteryManagementSystem.Batteries[i].Cycles =
                    _batteryManagementSystem.Batteries[i].ChargedTotal * 1000 / 5120m;
            }
        }
    }

    private void UpdateBmsData(CANFrame canFrame)
    {
        for (int i = 0; i < _batteryManagementSystem.Batteries.Count; i++)
        {
            if (canFrame.FrameId.Equals("0351"))
            {
                _batteryManagementSystem.ChargeVoltage = canFrame.DataInShorts[0] / 10m;
                _batteryManagementSystem.ChargeCurrentLimit = canFrame.DataInShorts[1] / 10m;
                _batteryManagementSystem.ChargeCurrentLimitMax = canFrame.DataInShorts[2] / 10m;
                _batteryManagementSystem.BatteryCutoffVoltage = canFrame.DataInShorts[3] / 10m;
                continue;
            }

            if (canFrame.FrameId.Equals("0371"))
            {
                _batteryManagementSystem.CurrentLimit = canFrame.DataInShorts[0] / 10m;
                _batteryManagementSystem.DischargeLimit = canFrame.DataInShorts[1] / 10m;
                continue;
            }

            if (canFrame.FrameId.Equals("0355"))
            {
                _batteryManagementSystem.StateOfCharge = canFrame.DataInShorts[0] / 1m;
                _batteryManagementSystem.StateOfHealth = canFrame.DataInShorts[1] / 1m;
                continue;
            }

            if (canFrame.FrameId.Equals("0356"))
            {
                _batteryManagementSystem.Voltage = canFrame.DataInShorts[0] / 100m;
                _batteryManagementSystem.Amps = canFrame.DataInShorts[1] / -10m;
                _batteryManagementSystem.Temperature = canFrame.DataInShorts[2] / 10m;
                _batteryManagementSystem.Watts = _batteryManagementSystem.Voltage * _batteryManagementSystem.Amps / 1m;
                continue;
            }

            if (canFrame.FrameId.Equals("0361"))
            {
                _batteryManagementSystem.CellVoltageHigh = canFrame.DataInShorts[0] / 1000m;
                _batteryManagementSystem.CellVoltageLow = canFrame.DataInShorts[1] / 1000m;
                _batteryManagementSystem.BmsTemperatureHigh = canFrame.DataInShorts[2] / 10m;
                _batteryManagementSystem.BmsTemperatureLow = canFrame.DataInShorts[3] / 10m;
                _batteryManagementSystem.CellVoltageDelta =
                    canFrame.DataInShorts[0] / 1000m - canFrame.DataInShorts[1] / 1000m;
                continue;
            }

            if (canFrame.FrameId.Equals("0363"))
            {
                _batteryManagementSystem.BatteryCapacity = canFrame.DataInShorts[0] / 10m;
                _batteryManagementSystem.FullChargedRestingVoltage = canFrame.DataInShorts[1] / 10m;
            }
        }
    }

    private void StatusUpdateAndAddBatteries(CANFrame canFrame)
    {
        if (!canFrame.FrameId.Equals("0364")) return;
        _batteryManagementSystem.Statuses = new BmsStatuses
        {
            Status1 = canFrame.Data[10],
            Status2 = canFrame.Data[11],
            Status3 = canFrame.Data[12],
            Status4 = canFrame.Data[13],
            Status5 = canFrame.Data[14],
            Status6 = canFrame.Data[15],
            Status7 = canFrame.Data[16],
            Status8 = canFrame.Data[17]
        };
        if (_batteryCountSet) return;
        _batteryCountSet = true;
        var batCount = _batteryManagementSystem.Statuses.Status5;
        _batteryManagementSystem.Batteries = new List<Battery>(batCount);
        var batteries = new List<Battery>(batCount);
        for (var i = 0; i < batCount; i++)
        {
            var battery = new Battery();
            batteries.Add(battery);
        }

        _batteryManagementSystem.Batteries.AddRange(batteries);
    }
}
