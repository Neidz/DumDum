namespace DumDumLibrary.Logger;

public enum LogLevel
{
    Info,
    Error,
    Debug,
    Warning,
    Fatal,

}

public interface ILogger
{
    void Log(LogLevel level, string message);
}