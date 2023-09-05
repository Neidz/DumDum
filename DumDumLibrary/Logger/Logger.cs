namespace DumDumLibrary.Logger;

public class Logger : ILogger
{
    private string FormatLogMessage(LogLevel level, string message)
    {
        return $"[{level}] {message}";
    }

    public void Log(LogLevel level, string message)
    {
        Console.WriteLine(FormatLogMessage(level, message));
    }
}