using Microsoft.AspNetCore.Mvc;

namespace DotSyncServer.Services;

public class DownloadService : IDownloadService
{
    private string _rootFolder;

    public DownloadService(IConfiguration configuration)
    {
        _rootFolder = configuration.GetSection("StorageFolder").Value;
    }

    public PhysicalFileResult DownloadFile(string fileName)
    {
        var filePath = Path.Combine(_rootFolder, fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        // This may not work with large (GBs) file download, need do more testing
        return new PhysicalFileResult(filePath, "application/octet-stream");
    }
}
