using FSync.Logger.Interfaces;
using FSync.SyncCore.Interfaces;
using FSync.SyncCore.Models;

namespace FSync.SyncCore;

public class Synchronizer
{
    private readonly IFolderComparer iFolderComparer;
    private readonly ILogger iLogger;

    public Synchronizer(IFolderComparer folderComparer, ILogger logger)
    {
        iFolderComparer = folderComparer ?? throw new ArgumentNullException(nameof(folderComparer));
        iLogger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public FolderDiff Sync(string mainFolderPath, string replicaFolderPath)
    {
        FolderDiff diff = iFolderComparer.CompareFolders(mainFolderPath, replicaFolderPath);

        foreach (var mainFile in diff.Removed)
        {
            iLogger.LogInfo($"Delete {Path.Combine(replicaFolderPath, mainFile.Path)}");
            DeleteEntry(Path.Combine(replicaFolderPath, mainFile.Path));
        }
            
        foreach (var mainFile in diff.Changed)
        {
            string replicaPath = Path.Combine(replicaFolderPath, mainFile.Path);

            if (mainFile.isDirectory || Directory.Exists(replicaPath))
            {
                iLogger.LogInfo($"Delete {replicaPath}");
                DeleteEntry(replicaPath);
            }

            iLogger.LogInfo($"Copy {mainFile.Path} from {mainFolderPath} to {replicaFolderPath}");
            CopyEntry(mainFolderPath, replicaFolderPath, mainFile);
        }

        foreach (var mainFile in diff.Missing)
        {
            iLogger.LogInfo($"Copy {mainFile.Path} from {mainFolderPath} to {replicaFolderPath}");
            CopyEntry(mainFolderPath, replicaFolderPath, mainFile);
        }

        return diff;
    }

    private static void CopyEntry(string source, string target, FileState fileState)
    {
        string sourcePath = Path.Combine(source, fileState.Path);
        string targetPath = Path.Combine(target, fileState.Path);

        if (fileState.isDirectory)
        {
            Directory.CreateDirectory(targetPath);
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
        File.Copy(sourcePath, targetPath, overwrite: true);
        File.SetLastWriteTimeUtc(targetPath, fileState.LastModified);
    }

    private static void DeleteEntry(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path, recursive: true);
        else if (File.Exists(path))
            File.Delete(path);
    }
}
