namespace FSync.SyncCore.Models;

public record FileState(string Path, long Size, DateTime LastModified, bool isDirectory = false)
{
    public override string ToString() // auto generated in IDE
    {
        return $"{Path} | Size: {Size} bytes | Last Modified: {LastModified} | Is Directory: {isDirectory}";
    }
}
