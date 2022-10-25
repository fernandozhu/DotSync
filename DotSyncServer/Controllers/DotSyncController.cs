using Microsoft.AspNetCore.Mvc;

namespace DotSyncServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DotSyncController : ControllerBase
{
    private readonly ILogger<string> _logger;

    public DotSyncController(ILogger<string> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "photo")]
    public IActionResult Get()
    {

        return Ok("Hello");
    }
}
