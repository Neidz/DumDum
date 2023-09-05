using DumDumLibrary.SerialCommunication;

class ConsoleController
{
    public static void Main()
    {
        ISerialCommunication serialPort = new MockedSerialCommunication();

        string command1 = "#1P500#2P500#3P500T300D0\r\n";
        string command2 = "#1P2000#2P2000#3P2000T300D0\r\n";

        string[] commands = { command1, command2 };

        try
        {
            serialPort.Open();

            while (true)
            {
                foreach (string message in commands)
                {
                    serialPort.SendCommand(message);

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
            serialPort.Close();
        }
    }
}