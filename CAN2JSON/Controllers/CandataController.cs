using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CAN2JSON.Controllers;

[ApiController]
[Route("[controller]")]
public class CandataController : ControllerBase
{
   private readonly ILogger<CandataController> _logger;
    private readonly ApplicationInstance _application;

    public CandataController(ILogger<CandataController> logger, ApplicationInstance application)
    {
        _application = application;
        _logger = logger;
    }

    [HttpGet(Name = "GetBatteryData")]
    public JsonObject? Get()
    {
        return _application.Application["json"] as JsonObject;
    }
}