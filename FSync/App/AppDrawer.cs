using FSync.Logger.Interfaces;
using FSync.SyncCore;

namespace FSync.App;

public class AppDrawer
{
    private readonly Synchronizer _synchronizer;
    private readonly ILogger _logger;

    public AppDrawer(Synchronizer synchronizer, ILogger logger)
    {
        _synchronizer = synchronizer;
        _logger = logger;
    }

    public async Task DrawAppAsync(string mainFolderPAth, string replicaFolderPath, int repeatDelay, string logFilePath)
    {
        _logger.LogInfo($"Starting sync between '{mainFolderPAth}' and '{replicaFolderPath}' every {repeatDelay} seconds. Press ESC to exit.");
        _logger.LogInfo($"Logging to '{logFilePath}'.");

        while (true)
        {
            try
            {
                var diff = _synchronizer.Sync(mainFolderPAth, replicaFolderPath);
                _logger.LogInfo($"Sync completed. Removed: {diff.Removed.Count}, Changed: {diff.Changed.Count}, Missing: {diff.Missing.Count}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during sync: {ex.Message}");
            }

            var waitUntil = DateTime.UtcNow.AddSeconds(repeatDelay);
            while (DateTime.UtcNow < waitUntil)
            {
                Console.Write(".");
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    return;

                await Task.Delay(100);
            }
            Console.WriteLine();
        }
    }
}