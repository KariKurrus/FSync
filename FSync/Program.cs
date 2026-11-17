using FSync.App;
using FSync.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSync;

internal class Program
{
    static async Task Main(string[] args)
    {
        var mainFolderPath = args.ElementAtOrDefault(0) ?? throw new ArgumentException("Please provide the path to the main folder as the first argument.");
        var replicaFolderPath = args.ElementAtOrDefault(1) ?? throw new ArgumentException("Please provide the path to the replica folder as the second argument.");
        var logFilePath = args.ElementAtOrDefault(2) ?? throw new ArgumentException("Please provide the path to the log file as the third argument.");
        var repeatDelay = int.Parse(args.ElementAtOrDefault(3) ?? "30");

        using var provider = new ServiceCollection()
                .ConfigureServices(logFilePath: logFilePath)
                .BuildServiceProvider();

        await provider.GetRequiredService<AppDrawer>().DrawAppAsync(mainFolderPath, replicaFolderPath  , repeatDelay, logFilePath);
    }
}