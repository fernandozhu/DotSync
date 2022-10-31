using Microsoft.AspNetCore.Mvc;

namespace DotSyncServer.Services;

public class DownloadService : IDownloadService
{
    private IFileService _fileService;

    public DownloadService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public PhysicalFileResult DownloadFile(string fileName)
    {
        string filePath = _fileService.GetFilePath(fileName);
        string mimeType = _fileService.GetMimeType(fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        // This may not work with large (GBs) file download, need do more testing
        return new PhysicalFileResult(filePath, mimeType);
    }
}
