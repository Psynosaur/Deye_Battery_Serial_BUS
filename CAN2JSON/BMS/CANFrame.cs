using System.Text.Json.Nodes;

namespace CAN2JSON.BMS;

public class CANFrame
{
    public string FrameId { get; protected set; }
    public byte[] Data { get; protected set; }
    public byte DataSize { get; protected set; }
    public byte[][] DataBytes { get; protected set; }

    public short[] DataInShorts { get; protected set; }

    public CANFrame(string frameId)
    {
        FrameId = frameId;
        DataInShorts = new short[4];
        Data = new byte[20];
        DataBytes = new byte[4][];
    }

    public void UpdateValues(byte[] values)
    {
        var dataOffset = 10;
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
        }
    }

    public JsonObject ToJson()
    {
        var json = new JsonObject();
        json["FrameId"] = FrameId;
        json["Data1"] = DataInShorts[0];
        json["Data2"] = DataInShorts[1];
        json["Data3"] = DataInShorts[2];
        json["Data4"] = DataInShorts[3];
        return json;
    }
}