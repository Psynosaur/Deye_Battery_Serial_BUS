using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace CAN2JSON.BMS;

public class CANFrame
{
    public string FrameId { get; protected set; }
    public byte[] Data { get; protected set; }
    public byte DataSize { get; protected set; }
    public byte[][] DataBytes { get; protected set; }

    public string DataHexString { get; protected set; }

    public short[] DataInShorts { get; protected set; }
    public int[] DataInInt32 { get; protected set; }

    public DateTime LastUpdated { get; set; } // N

    public CANFrame(string frameId)
    {
        FrameId = frameId;
        DataInShorts = new short[4];
        Data = new byte[20];
        DataBytes = new byte[4][];
        LastUpdated = DateTime.Now;
        DataHexString = "";
        DataInInt32 = new int[2];
    }

    public void UpdateValues(byte[] values)
    {
        var dataOffset = 10;
        DataHexString = BitConverter.ToString(values).Replace("-", " ");
        if (values.Length == 20)
        {
            for (int i = 0; i < 20; i++)
            {
                Data[i] = values[i];
            }

            DataSize = Data[9];

            for (int i = 0; i < 4; i++)
            {
                DataBytes[i] = new[]
                {
                    Data[dataOffset],
                    Data[dataOffset + 1]
                };
                DataInShorts[i] = BitConverter.ToInt16(DataBytes[i]);
                dataOffset += 2;

            }

            dataOffset = 10;
            for (var i = 0; i < 2; i++)
            {
                var intArr =  new[]{
                    Data[dataOffset],
                    Data[dataOffset + 1],
                    Data[dataOffset + 2],
                    Data[dataOffset + 3],
                };
                DataInInt32[i] = BitConverter.ToInt32(intArr);
                dataOffset += 4;
            }
        }
    }

    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["FrameId"] = FrameId;
        json["Hex"] = DataHexString;
        json["DataSize"] = DataSize;
        json["Data1"] = DataInShorts[0];
        json["Data2"] = DataInShorts[1];
        json["Data3"] = DataInShorts[2];
        json["Data4"] = DataInShorts[3];
        json["LastUpdated"] = LastUpdated.ToString("yyyy-MM-dd HH:mm:ss");
        return json;
    }
}