namespace DumDumLibrary.SerialCommunication;
using System.IO.Ports;

public class SerialCommunication : ISerialCommunication
{
    private SerialPort _serialPort;

    public SerialCommunication(SerialPortConfig serialPortConfig)
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

    public void SendCommand(string data)
    {
        _serialPort.WriteLine(data);
    }
}