using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Threading;
using CAN2JSON.BMS;

namespace CAN2JSON;

public class SerialDataBackgroundService : BackgroundService
{
    private readonly ILogger<SerialDataBackgroundService> _logger;
    private readonly SerialPort _serialPort;
    private readonly Dictionary<string, FrameType> _frames;
    private readonly JsonObject _document;
    private readonly ApplicationInstance _application;
    private readonly BatteryManagementSystem _batteryManagementSystem = new(1);
    private bool _firstFrame = true;

    public SerialDataBackgroundService(ILogger<SerialDataBackgroundService> logger, ApplicationInstance application)
    {
        _logger = logger;
        _application = application;
        _document = new JsonObject();
        _serialPort = new SerialPort(false ? "/dev/ttyUSB0" : "COM6", 2000000);
        _frames = new Dictionary<string, FrameType>
        {
            { "000", new FrameType("000", new JsonObject()) }
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _application.Application["json"] = _document;
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
            _logger.LogError(ex, "An error occurred while processing serial data.");
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
            _logger.LogError(ex, "An error occurred while processing received data.");
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

            if (_firstFrame)
            {
                _batteryManagementSystem.CanFrames.Add(canFrame);
                _firstFrame = false;
            }
            if (!_batteryManagementSystem.CanFrames.Any(cf => cf.FrameId.Equals(canFrame.FrameId)))
            {
                _batteryManagementSystem.CanFrames.Add(canFrame);
            }
            var copyArr = _batteryManagementSystem.CanFrames;
            for (var index = 0; index < copyArr.Count; index++)
            {
                if (copyArr[index].FrameId == canFrame.FrameId)
                {
                    copyArr[index] = canFrame;
                }
            }

            _batteryManagementSystem.CanFrames = copyArr;


            // _logger.LogInformation($"Frames in BMS: {copyArr.Count}");

            var frameStringData =
                $"{canFrame.DataInShorts[0]}," +
                $" {canFrame.DataInShorts[1]}," +
                $" {canFrame.DataInShorts[2]}," +
                $" {canFrame.DataInShorts[3]}";
            // Update values of existing frame in collection
            if (_frames.ContainsKey(id))
            {
                string frameId = id;
                var jsonObj = new JsonObject();
                FrameType frame = _frames[frameId];
                jsonObj.Add("data", frameStringData);
                frame.Data = jsonObj;
            }

            foreach (var frame in _frames.Values)
            {
                var frameJson = frame.ToJson();
                if (frameJson.Count > 0)
                {
                    if (!_document.ContainsKey(frame.FrameId))
                    {
                        _document.Add(frame.FrameId, frameJson);
                        return;
                    }

                    if (_document.ContainsKey(frame.FrameId))
                    {
                        _document.Remove(frame.FrameId);
                        _document.Add(frame.FrameId, frameJson);
                    }
                }
            }

            // Add frame if not in frame collection already
            if (!_frames.ContainsKey(id))
            {
                var jsonObj = new JsonObject();
                jsonObj.Add("data", frameStringData);
                _frames.Add(id, new FrameType(id, jsonObj));
            }

            _application.Application["json"] = _batteryManagementSystem.ToJson();
        }
        catch (Exception ex)
        {
            var errFrameData = BitConverter.ToString(receivedData);
            _logger.LogError(ex, errFrameData);
        }
    }
}

public class FrameType
{
    public string FrameId { get; protected set; }
    public JsonObject Data { get; set; }

    public FrameType(string frameId, JsonObject data)
    {
        FrameId = frameId;
        Data = data;
    }

    public JsonObject ToJson()
    {
        return Data.AsObject();
    }
}