using Microsoft.AspNetCore.StaticFiles;

namespace DotSyncServer.Services;

public class FileService : IFileService
{
    private string _rootFolder = "";

    public FileService(IConfiguration configuration)
    {
        _rootFolder = configuration.GetSection("StorageFolder").Value;
        if (string.IsNullOrEmpty(_rootFolder))
        {
            throw new Exception("StorageFolder is not configured");
        }
    }

    public string GetRootFolder()
    {
        return _rootFolder;
    }

    public string GetFilePath(string fileName)
    {
        return Path.Combine(_rootFolder, fileName);
    }

    public string GetMimeType(string fileName)
    {
        string filePath = GetFilePath(fileName);
        var provider = new FileExtensionContentTypeProvider();
        provider.TryGetContentType(filePath, out var mimeType);
        return mimeType ?? "";
    }
}
