namespace DumDumLibrary.Communication;

public interface ISerialPort
{
    bool IsOpen { get; }
    void Open();
    void Close();
    void Write(string data);
    void WriteLine(string data);
}