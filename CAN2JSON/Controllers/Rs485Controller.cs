using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CAN2JSON.Controllers;

[ApiController]
[Route("[controller]")]
public class Rs485Controller : ControllerBase
{
   private readonly ILogger<Rs485Controller> _logger;
    private readonly ApplicationInstance _application;

    public Rs485Controller(ILogger<Rs485Controller> logger, ApplicationInstance application)
    {
        _application = application;
        _logger = logger;
    }

    [HttpGet(Name = "GetBatteryCells")]
    public JsonObject? Get()
    {
        return _application.Application["jsonSerial"] as JsonObject;
    }
}