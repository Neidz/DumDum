using System.IO.Ports;

class DumDum
{


    public static void Main()
    {
        SerialPort serialPort = new()
        {
            PortName = "/dev/ttyACM0",
            BaudRate = 115200,
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One
        };

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
                    serialPort.WriteLine(message);

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