using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Ports;
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

    public SerialDataBackgroundService(ILogger<SerialDataBackgroundService> logger)
    {
        _logger = logger;
        _serialPort = new SerialPort(true ? "/dev/ttyUSB0" : "COM8", 2000000);
        _frames = new Dictionary<string, FrameType>
        {
            // Define your frame types here
            { "361", new FrameType305() },
            { "351", new FrameType351() },
            // Add more frame types as needed
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _serialPort.Open();
            _serialPort.DataReceived += SerialPort_DataReceived;

            while (!stoppingToken.IsCancellationRequested)
            {
                // Process serial data continuously
                await Task.Delay(500, stoppingToken); // Adjust the delay as needed
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
        if (bytes != 20 && !(buffer[5] == 0x56 && buffer[6] == 0x03)) return;
        List<byte> littleBytes = new List<byte>(bytes);
        foreach (var b in buffer)
        {
            var swp = BinaryPrimitives.ReverseEndianness(b);
            littleBytes.Add(swp);
        }
        // Array.Reverse(buffer);
        var receivedData = BitConverter.ToString(buffer); // Read the received data from the serial port

        try
        {
            ProcessReceivedData(receivedData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing received data.");
        }
    }
    public static int SwapEndianness(int value)
    {
        var b1 = (value >> 0) & 0xff;
        var b2 = (value >> 8) & 0xff;
        var b3 = (value >> 16) & 0xff;
        var b4 = (value >> 24) & 0xff;

        return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
    }
    private void ProcessReceivedData(string receivedData)
    {
        _logger.LogInformation(receivedData);
        // Parse the received data and update the corresponding frame values
        string[] dataParts = receivedData.Split('-'); // Adjust the delimiter as needed

        // var document = new JsonObject();
        //
        // foreach (string dataPart in dataParts)
        // {
        //     string[] frameData = dataPart.Split(',');
        //
        //     if (frameData.Length >= 2 && _frames.ContainsKey(frameData[0]))
        //     {
        //         string frameId = frameData[0];
        //         FrameType frame = _frames[frameId];
        //         frame.UpdateValues(frameData.Skip(1).ToArray());
        //     }
        // }
        //
        // foreach (var frame in _frames.Values)
        // {
        //     var frameJson = frame.ToJson();
        //     if (frameJson.Count > 0)
        //     {
        //         if (!document.ContainsKey(frame.FrameId))
        //         {
        //             document.Add(frame.FrameId, frameJson);
        //         }
        //         else
        //         {
        //             var existingFrameJson = document[frame.FrameId] as JsonObject;
        //             if (existingFrameJson != null)
        //             {
        //                 foreach (var kvp in frameJson)
        //                 {
        //                     existingFrameJson[kvp.Key] = kvp.Value;
        //                 }
        //             }
        //         }
        //     }
        // }
        //
        // var jsonString = document.ToString();
        // _logger.LogInformation("Current frame values:\n" + jsonString);
    }
}
public abstract class FrameType
{
    public string FrameId { get; protected set; }
    protected JsonObject Data { get; set; }

    public FrameType(string frameId)
    {
        FrameId = frameId;
        Data = new JsonObject();
    }

    public void UpdateValues(string[] values)
    {
        // Update the values for this frame
        if (values.Length > 0)
        {
            Data = new JsonObject(); // Clear the existing data

            for (int i = 0; i < values.Length; i++)
            {
                Data["Field" + (i + 1)] = values[i];
            }
        }
    }

    public JsonObject ToJson()
    {
        return Data.AsObject();
    }
}

public class FrameType305 : FrameType
{
    public FrameType305() : base("361")
    {
        // Add any additional properties specific to FrameType305 if needed
    }
}

public class FrameType351 : FrameType
{
    public FrameType351() : base("351")
    {
        // Add any additional properties specific to FrameType351 if needed
    }
}

// Add more frame type classes as needed