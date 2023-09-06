using DumDumLibrary.SerialCommunication;
using DumDumLibrary.Logger;

class ConsoleController
{
    public static void Main(string[] args)
    {
        bool IsMocked = args.Length > 0 && args[0] == "--mock";

        ISerialCommunication serialCommunication = IsMocked ? new MockedSerialCommunication(new Logger()) : new SerialCommunication(new Logger(), new SerialPortConfig());

        int time = 1000;
        int delay = 200;

        string command0 = $"#2A0T{time}\r\n";
        string command1 = $"#2A90T{time}\r\n";
        string command2 = $"#2A180T{time}\r\n";

        try
        {
            serialCommunication.Open();

            while (true)
            {

                serialCommunication.SendCommand(command0);
                Thread.Sleep(time + delay);
                serialCommunication.SendCommand(command1);
                Thread.Sleep(time + delay);
                serialCommunication.SendCommand(command0);
                Thread.Sleep(time + delay);
                serialCommunication.SendCommand(command2);
                Thread.Sleep(time + delay);
                Console.WriteLine("Starting over.");

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