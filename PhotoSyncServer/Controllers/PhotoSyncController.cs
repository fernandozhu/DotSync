using Microsoft.AspNetCore.Mvc;

namespace PhotoSyncServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoSyncController : ControllerBase
{
    private readonly ILogger<string> _logger;

    public PhotoSyncController(ILogger<string> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "photo")]
    public IActionResult Get()
    {

        return Ok("Hello");
    }
}
