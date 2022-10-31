using Microsoft.AspNetCore.Mvc;

namespace DotSyncServer.Services;

public interface IDownloadService
{
    public PhysicalFileResult DownloadFile(string fileName);
}
