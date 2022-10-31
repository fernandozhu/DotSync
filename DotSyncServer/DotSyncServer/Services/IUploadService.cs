
using Microsoft.AspNetCore.WebUtilities;

namespace DotSyncServer.Services;

public interface IUploadService
{
    public Task<bool> SaveFile(MultipartReader reader, MultipartSection? section);
}
