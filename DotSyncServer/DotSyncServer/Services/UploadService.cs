using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace DotSyncServer.Services;

public class UploadService : IUploadService
{
    private readonly IFileService _fileService;

    public UploadService(IFileService fileService)
    {
        _fileService = fileService;
    }


    public async Task<bool> SaveFile(MultipartReader reader, MultipartSection? section)
    {
        var nextSection = section;
        string rootFolder = _fileService.GetRootFolder();

        while (nextSection != null)
        {
            nextSection = await SaveFileSection(rootFolder, reader, nextSection);
        }

        return true;
    }

    private async Task<MultipartSection?> SaveFileSection(string filePath, MultipartReader reader, MultipartSection? section)
    {
        if (section == null) return null;

        bool isHeaderValid = ValidateSectionHeader(section, out var fileName);

        if (isHeaderValid)
        {
            using (var fileStream = File.Create(Path.Combine(filePath, fileName)))
            {
                await section.Body.CopyToAsync(fileStream);
            }
        }

        return await reader.ReadNextSectionAsync();
    }


    // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Disposition
    private bool ValidateSectionHeader(MultipartSection section, out string fileName)
    {
        var valid = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
        fileName = "";


        if (!valid || contentDisposition == null)
        {
            return false;
        }

        fileName = (contentDisposition.FileName ?? "").Trim('"');
        bool isFormData = contentDisposition.DispositionType.Equals("form-data");
        bool hasFileName = !string.IsNullOrEmpty(fileName);

        return isFormData && hasFileName;
    }
}
