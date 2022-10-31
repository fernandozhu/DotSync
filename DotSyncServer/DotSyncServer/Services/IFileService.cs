namespace DotSyncServer.Services;

public interface IFileService
{
    public string GetRootFolder();
    public string GetFilePath(string fileName);
    public string GetMimeType(string fileName);
}
