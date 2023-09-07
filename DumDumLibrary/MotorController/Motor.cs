namespace DumDumLibrary.MotorController;

public class Motor
{
    public int Number { get; set; }
    public int Angle { get; set; } = 90;
    public int DefaultAngle = 90;
    public int MaxAngle { get; set; } = 180;
    public int MinAngle { get; set; } = 0;
    public bool Busy { get; set; } = false;
}