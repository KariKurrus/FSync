using FSync.SyncCore.Models;

namespace FSync.SyncCore.Helpers;

internal static class FolderScanner
{
    public static Dictionary<string, FileState> GetFileStates(string folderPath)
    {
        var fileStates = new Dictionary<string, FileState>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var dir = new DirectoryInfo(folderPath);

            foreach (var entry in dir.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(folderPath, entry.FullName);

                fileStates[relativePath] = entry is FileInfo file
                    ? new FileState(relativePath, file.Length, file.LastWriteTimeUtc)
                    : new FileState(relativePath, 0, default, isDirectory: true);
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Folder {folderPath} is not found.");
            return new Dictionary<string, FileState>(StringComparer.OrdinalIgnoreCase);
        }

        return fileStates;
    }
}
