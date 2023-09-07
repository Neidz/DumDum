namespace DumDumLibrary.MotorController;

public interface IMotorController
{
    void MoveMotor(MotorCommand motorCommand, int time);
    void MoveMultipleMotors(List<MotorCommand> motorCommands, int time);
    void ResetMotorsToInitialPositions(int time);
    Motor GetMotor(int motorNumber);
}