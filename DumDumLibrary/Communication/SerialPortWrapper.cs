namespace DumDumLibrary.Communication;
using System.IO.Ports;

public class SerialPortWrapper : ISerialPort
{
    private SerialPort _serialPort;

    public SerialPortWrapper(SerialPortConfig serialPortConfig)
    {
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
        _serialPort.Open();
    }

    public void Close()
    {
        _serialPort.Close();
    }

    public void Write(string data)
    {
        _serialPort.Write(data);
    }

    public void WriteLine(string data)
    {
        _serialPort.WriteLine(data);
    }

    public string ReadLine()
    {
        return _serialPort.ReadLine();
    }
}