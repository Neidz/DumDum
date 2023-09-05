namespace DumDumLibrary.SerialCommunication;

public interface ISerialCommunication
{
    bool IsOpen { get; }
    void Open();
    void Close();
    void SendCommand(string data);
}