using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;

namespace CAN2JSON.BMS;

public class Canmon3000
{
    public Canmon3000(string xmlTemplate)
    {
        XmlTemplate = xmlTemplate;
    }

    public string XmlTemplate { get; set; }

    public JsonNode? XmlJnode { get; set; }
    
    // TODO: loop through members of xml from CAN Monitor 3000 and create BMS attributes programmatically 

    public static string ConvertXmlToJson(string xmlContent)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        string json = JsonSerializer.Serialize(xmlDoc.DocumentElement, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return json;
    }

    public void ParseMessageTreeNodes()
    {
        XmlJnode = JsonNode.Parse(XmlConvert.XmlToJSON(XmlTemplate));
    }

    public void FindJsonObjectByPropertyName()
    {
        if (XmlJnode?.AsObject() is { } temp)
            foreach (var jn in temp)
            {
                if (jn.Key.Equals("HeaderTreeNode")) Console.WriteLine($"{jn.Key} {jn.Value}");
            }
    }
    
    public JsonObject ToJson()
    {
        // TODO: Do something here with CAN template
        var json = new JsonObject();
        ParseMessageTreeNodes();
        FindJsonObjectByPropertyName();
        json["XmlTemplate"] = XmlJnode;
        return json;
    }
}