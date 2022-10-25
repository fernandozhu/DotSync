using Microsoft.AspNetCore.Mvc;
using DotSyncServer.Models;
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
        var file = new SyncFile("/Users/fernandozhu/Desktop/hello.avi");

        // return Ok($"MIME: {file.MimeType}");
        return Ok(file);
    }
}
