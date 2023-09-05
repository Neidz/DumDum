namespace DumDumLibrary.SerialCommunication;
using System.IO.Ports;
using DumDumLibrary.Logger;

public class SerialCommunication : ISerialCommunication
{
    private readonly SerialPort _serialPort;
    private readonly ILogger _logger;

    public SerialCommunication(ILogger logger, SerialPortConfig serialPortConfig)
    {
        _logger = logger;

        _serialPort = new()
        {
            PortName = serialPortConfig.PortName,
            BaudRate = serialPortConfig.BaudRate,
            Parity = serialPortConfig.Parity,
            DataBits = serialPortConfig.DataBits,
            StopBits = serialPortConfig.StopBits

        };
    }

    public bool IsOpen => _serialPort.IsOpen;

    public void Open()
    {
        _logger.Log(LogLevel.Info, "Opening serial communication.");
        _serialPort.Open();
    }

    public void Close()
    {
        _logger.Log(LogLevel.Info, "Closing serial communication.");
        _serialPort.Close();
    }

    public void SendCommand(string data)
    {
        if (!IsOpen)
        {
            _logger.Log(LogLevel.Fatal, "Serial communication is not open. Command not sent.");
            throw new InvalidOperationException("Serial communication is not open.");
            return;
        }
        _serialPort.WriteLine(data);
    }
}