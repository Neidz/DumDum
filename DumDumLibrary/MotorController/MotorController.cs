namespace DumDumLibrary.MotorController;

using DumDumLibrary.Logger;
using DumDumLibrary.SerialCommunication;

public class MotorController : IMotorController
{
    readonly ISerialCommunication _serialCommunication;
    List<Motor> _motors;
    ILogger _logger;
    public MotorController(ISerialCommunication serialCommunication, List<Motor> motors, ILogger logger)
    {
        _serialCommunication = serialCommunication;
        _motors = motors;
        _logger = logger;
    }

    private static string CreateSerialCommand(MotorCommand motorCommand, int time)
    {
        return $"#{motorCommand.MotorNumber}A{motorCommand.Angle}T{time}\r\n";
    }

    private static string CreateSerialCommand(List<MotorCommand> motorCommands, int time)
    {
        string formattedCommandsForMotors = "";

        foreach (MotorCommand motorCommand in motorCommands)
        {
            formattedCommandsForMotors += $"#{motorCommand.MotorNumber}A{motorCommand.Angle}";
        }

        return $"{formattedCommandsForMotors}T{time}\r\n";
    }

    public Motor GetMotor(int motorNumber)
    {
        Motor? motor = _motors.Find((motor) => motor.Number == motorNumber);

        if (motor == null)
        {
            _logger.Log(LogLevel.Fatal, $"Motor with number {motorNumber} not found.");
            throw new Exception($"Motor with number {motorNumber} not found.");
        }
        return motor;
    }

    public void MoveMotor(MotorCommand motorCommand, int time)
    {
        Motor motor = GetMotor(motorCommand.MotorNumber);

        motor.Angle = motorCommand.Angle;

        string serialCommand = CreateSerialCommand(motorCommand, time);
        _logger.Log(LogLevel.Debug, "Sending command throgh serial: " + serialCommand);
        _serialCommunication.SendCommand(serialCommand);
    }

    public void MoveMultipleMotors(List<MotorCommand> motorCommands, int time)
    {
        foreach (MotorCommand motorCommand in motorCommands)
        {
            Motor motor = GetMotor(motorCommand.MotorNumber);

            motor.Angle = motorCommand.Angle;
        }

        string serialCommand = CreateSerialCommand(motorCommands, time);
        _logger.Log(LogLevel.Debug, "Sending command throgh serial: " + serialCommand);
        _serialCommunication.SendCommand(serialCommand);
    }

    public void ResetMotorsToInitialPositions(int time)
    {
        List<MotorCommand> motorCommands = new();

        foreach (Motor motor in _motors)
        {
            motor.Angle = motor.DefaultAngle;

            motorCommands.Add(new() { MotorNumber = motor.Number, Angle = motor.Angle });
        }

        string serialCommand = CreateSerialCommand(motorCommands, time);
        _logger.Log(LogLevel.Debug, "Sending command throgh serial: " + serialCommand);
        _serialCommunication.SendCommand(serialCommand);
    }
}