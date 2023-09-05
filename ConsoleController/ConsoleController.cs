using DumDumLibrary.SerialCommunication;
using DumDumLibrary.Logger;

class ConsoleController
{
    public static void Main(string[] args)
    {
        bool IsMocked = args.Length > 0 && args[0] == "--mock";

        ISerialCommunication serialCommunication = IsMocked ? new MockedSerialCommunication(new Logger()) : new SerialCommunication(new Logger(), new SerialPortConfig());

        string command1 = "#1P500#2P500#3P500T300D0\r\n";
        string command2 = "#1P2000#2P2000#3P2000T300D0\r\n";

        string[] commands = { command1, command2 };

        try
        {
            serialCommunication.Open();

            while (true)
            {
                foreach (string message in commands)
                {
                    serialCommunication.SendCommand(message);

                    Thread.Sleep(500);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            serialCommunication.Close();
        }
    }
}