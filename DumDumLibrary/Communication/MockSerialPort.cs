namespace DumDumLibrary.Communication;

public class MockSerialPort : ISerialPort
{
    public bool IsOpen { get; private set; }

    public void Open()
    {
        IsOpen = true;
    }

    public void Close()
    {
        IsOpen = false;
    }

    public void Write(string data)
    {
        if (!IsOpen)
        {
            throw new InvalidOperationException("Serial port is not open.");
        }
    }

    public void WriteLine(string data)
    {
        if (!IsOpen)
        {
            throw new InvalidOperationException("Serial port is not open.");
        }
    }
}