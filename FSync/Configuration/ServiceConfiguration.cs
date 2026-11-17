using FSync.App.Configuration;
using FSync.Logger.Configuration;
using FSync.SyncCore.Configuration;
using FSync.SyncCore.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace FSync.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, FSyncMode syncMode = FSyncMode.FileState, string logFilePath = "")
    {
        serviceCollection.AddFSync(syncMode);
        serviceCollection.AddLogger(logFilePath);
        serviceCollection.AddApp();

        return serviceCollection;
    }
}
