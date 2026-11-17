using FSync.SyncCore.Models;

namespace FSync.SyncCore.Interfaces;

public interface IFolderComparer
{
    public FolderDiff CompareFolders(string mainFolderPath, string replicaFolderPath);
}
    