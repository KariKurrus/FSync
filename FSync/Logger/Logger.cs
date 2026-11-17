using FSync.Logger.Interfaces;

namespace FSync.Logger;

public class Logger : ILogger
{
    private readonly string _logFilePath;
    public Logger(string logFilePath)
    {
        _logFilePath = logFilePath;

        if (!File.Exists(_logFilePath))
            File.Create(_logFilePath).Dispose();
    }

    public void LogError(string message)
    {
        Log(message, "ERROR");
    }

    public void LogInfo(string message)
    {
        Log(message, "INFO");
    }

    private void Log(string message, string logLevel)
    {
        string line = $"{DateTime.Now} [{logLevel}] {message}";
        Console.WriteLine(line);
        File.AppendAllText(_logFilePath, line + Environment.NewLine);
    }
}
