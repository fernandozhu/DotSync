using Microsoft.AspNetCore.Mvc;
using DotSyncServer.Services;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace DotSyncServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DotSyncController : ControllerBase
{
    private readonly ILogger<string> _logger;
    private readonly IUploadService _uploadService;
    private readonly IDownloadService _downloadService;

    public DotSyncController(ILogger<string> logger, IUploadService uploadService, IDownloadService downloadService)
    {
        _logger = logger;
        _uploadService = uploadService;
        _downloadService = downloadService;
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    [Route("upload")]
    public async Task<IActionResult> FileUpload()
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

    [HttpGet]
    [Route("download/{fileName}")]
    public PhysicalFileResult FileDownload(string fileName)
    {
        return _downloadService.DownloadFile(fileName);
    }
}
