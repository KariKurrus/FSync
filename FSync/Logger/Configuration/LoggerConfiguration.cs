using FSync.Logger.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FSync.Logger.Configuration;

public static class LoggerConfiguration
{
    public static IServiceCollection AddLogger(this IServiceCollection serviceCollection, string logFilePath)
    {
        serviceCollection.AddSingleton<ILogger> (new Logger(logFilePath));

        return serviceCollection;
    }
}
