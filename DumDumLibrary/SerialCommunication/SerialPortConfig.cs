namespace DumDumLibrary.SerialCommunication;
using System.IO.Ports;

public class SerialPortConfig
{
    public string PortName { get; set; } = "/dev/ttyACM0";
    public int BaudRate { get; set; } = 115200;

    public Parity Parity { get; set; } = Parity.None;
    public int DataBits { get; set; } = 8;
    public StopBits StopBits { get; set; } = StopBits.One;
}