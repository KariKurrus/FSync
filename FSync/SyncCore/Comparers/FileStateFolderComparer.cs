using FSync.SyncCore.Extensions;
using FSync.SyncCore.Helpers;
using FSync.SyncCore.Interfaces;
using FSync.SyncCore.Models;

namespace FSync.SyncCore.Comparer;

internal class FileStateFolderComparer : IFolderComparer
{
    public FolderDiff CompareFolders(string mainFolderPath, string replicaFolderPath)
    {
        var firstFolderStates = FolderScanner.GetFileStates(mainFolderPath);
        var secondFolderStates = FolderScanner.GetFileStates(replicaFolderPath);

        if (firstFolderStates.IsNullOrEmpty() || secondFolderStates.IsNullOrEmpty())
            return new FolderDiff(new List<FileState>(), new List<FileState>(), new List<FileState>());

        var missing = new List<FileState>();
        var changed = new List<FileState>();
        var removed = new List<FileState>();

        foreach (var (path, state1) in firstFolderStates)
        {
            if (!secondFolderStates.TryGetValue(path, out var state2))
                missing.Add(state1);
            else if (IsDifferent(state1, state2))
                changed.Add(state1);
        }

        foreach (var (path, state2) in secondFolderStates)
        {
            if (!firstFolderStates.ContainsKey(path))
                removed.Add(state2);
        }

        return new FolderDiff(missing, changed, removed);
    }

    private static bool IsDifferent(FileState a, FileState b) =>
        a.isDirectory != b.isDirectory ||
        a.Size != b.Size ||
        a.LastModified != b.LastModified;
}
