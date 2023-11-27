public class Enemy
{
    public Point Position { get; set; }
    public int PathIndex { get; set; }

    public double Speed { get; set; }

    public int TargetPointIndex { get; set; }
    public int StartTime { get; set; }
    public bool Stunned { get; set; }
    public int StunEndTime { get; set; }

}