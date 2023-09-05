namespace DumDumLibrary.SerialCommunication;
using DumDumLibrary.Logger;
public class MockedSerialCommunication : ISerialCommunication
{
    private readonly ILogger _logger;
    public MockedSerialCommunication(ILogger logger)
    {
        _logger = logger;
    }

    public bool IsOpen { get; private set; }

    public void Open()
    {
        _logger.Log(LogLevel.Info, "Opening mocked serial communication.");
        IsOpen = true;
    }

    public void Close()
    {
        _logger.Log(LogLevel.Info, "Closing mocked serial communication.");
        IsOpen = false;
    }

    public void SendCommand(string data)
    {
        if (!IsOpen)
        {
            _logger.Log(LogLevel.Fatal, "Mocked serial communication is not open. Command not sent.");
            throw new InvalidOperationException("Serial communication is not open.");
        }
    }
}