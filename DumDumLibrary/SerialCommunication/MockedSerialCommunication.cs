namespace DumDumLibrary.SerialCommunication;

public class MockedSerialCommunication : ISerialCommunication
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

    public void SendCommand(string data)
    {
        if (!IsOpen)
        {
            throw new InvalidOperationException("Serial communication is not open.");
        }
    }
}