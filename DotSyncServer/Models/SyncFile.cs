using Microsoft.AspNetCore.StaticFiles;
namespace DotSyncServer.Models;

class SyncFile
{
    public string Path { get; set; } = "";
    public string Name { get; private set; } = "";
    public string? MimeType { get; private set; }
    protected List<string> AllowedMimeTypes { get; } = new List<string>();
    public DateTime LastModified { get; private set; }

    public SyncFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        Path = path;
        ExtractFileProperties();
    }

    private bool Exists()
    {
        return File.Exists(Path);
    }
    private bool MatchMimeType()
    {
        if (AllowedMimeTypes.Count == 0)
        {
            throw new Exception($"No allowed MIME type for file type {this.GetType().Name}");
        }

        return AllowedMimeTypes.Contains(MimeType);
    }

    private void ExtractFileProperties()
    {
        // Get file MIME by file extension, a better approach would be to read the first 256 bytes of the file
        string? mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(Path, out mimeType);
        MimeType = mimeType;

        Name = System.IO.Path.GetFileName(Path);
        LastModified = File.GetLastWriteTime(Path);
    }
}
