using Microsoft.AspNetCore.Mvc;
using DotSyncServer.Services;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace DotSyncServer.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DotSyncController : ControllerBase
{
    private readonly IUploadService _uploadService;
    private readonly ILogger<string> _logger;

    public DotSyncController(ILogger<string> logger, IUploadService uploadService)
    {
        _logger = logger;
        _uploadService = uploadService;
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    [Route("upload")]
    public async Task<IActionResult> PostUpload()
    {
        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
        var reader = new MultipartReader(boundary, Request.Body);
        string response = string.Empty;
        var section = await reader.ReadNextSectionAsync();

        try
        {
            if (await _uploadService.SaveFile(reader, section))
            {
                Ok("*** File upload success");
            }
            else
            {
                Ok("*** File upload failed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Ok("FileUploaded");
    }
}
