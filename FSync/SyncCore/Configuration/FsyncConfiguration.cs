using FSync.SyncCore.Comparer;
using FSync.SyncCore.Comparers;
using FSync.SyncCore.Enums;
using FSync.SyncCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FSync.SyncCore.Configuration;

public static class FsyncConfiguration
{
    public static IServiceCollection AddFSync(this IServiceCollection serviceCollection, FSyncMode? syncMode = FSyncMode.FileState)
    {
        if(syncMode == FSyncMode.FileState)
            serviceCollection.AddSingleton<IFolderComparer, FileStateFolderComparer>();
        else
            serviceCollection.AddSingleton<IFolderComparer, HashSetFolderComparer>();

        serviceCollection.AddSingleton<Synchronizer>();

        return serviceCollection;
    }
}
