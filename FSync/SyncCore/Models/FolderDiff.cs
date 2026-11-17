namespace FSync.SyncCore.Models;

public record FolderDiff(List<FileState> Missing, List<FileState> Changed, List<FileState> Removed)
{
    public override string ToString() // auto generated in IDE
    {
        var missingStr = Missing.Count > 0 ? string.Join(Environment.NewLine, Missing) : "None";
        var changedStr = Changed.Count > 0 ? string.Join(Environment.NewLine, Changed) : "None";
        var removedStr = Removed.Count > 0 ? string.Join(Environment.NewLine, Removed) : "None";
        return $"Missing Files:{Environment.NewLine}{missingStr}{Environment.NewLine}" +
               $"Changed Files:{Environment.NewLine}{changedStr}{Environment.NewLine}" +
               $"Removed Files:{Environment.NewLine}{removedStr}";
    }
};
