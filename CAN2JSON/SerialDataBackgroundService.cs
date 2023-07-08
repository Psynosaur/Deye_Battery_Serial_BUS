using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Threading;

namespace CAN2JSON;

public class SerialDataBackgroundService : BackgroundService
{
    private readonly ILogger<SerialDataBackgroundService> _logger;
    private readonly SerialPort _serialPort;
    private readonly Dictionary<string, FrameType> _frames;
    private readonly JsonObject _document;
    private readonly ApplicationInstance _application;

    public SerialDataBackgroundService(ILogger<SerialDataBackgroundService> logger, ApplicationInstance application)
    {
        _logger = logger;
        _application = application;
        _document = new JsonObject();
        _serialPort = new SerialPort(true ? "/dev/ttyUSB0" : "COM8", 2000000);
        _frames = new Dictionary<string, FrameType>
        {
            {"000", new FrameType("000", new JsonObject())}
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
        List<byte> litteEndianBytes = new List<byte>(bytes);
        foreach (var b in buffer)
        {
            var swp = BinaryPrimitives.ReverseEndianness(b);
            litteEndianBytes.Add(swp);
        }
        try
        {
            ProcessReceivedData(litteEndianBytes.ToArray());
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
        byte[] data = new byte[8];
        Array.Copy(receivedData, 10, data , 0, 8);
        
        var id = $"Frame_{BitConverter.ToString(new[] { receivedData[6], receivedData[5] }).Replace("-", string.Empty)}";;
        var frameStringData =
            $"{BitConverter.ToInt16(new[] { data[0], data[1] })}, {BitConverter.ToInt16(new[] { data[2], data[3] })}, {BitConverter.ToInt16(new[] { data[4], data[5] })}, {BitConverter.ToInt16(new[] { data[6], data[7] })}";
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
        _application.Application["json"] = _document;
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
