public enum TowerType
{
    Melee,
    Ranged,
    Stun,
    Bomb
}

public class Tower
{
    public int Id { get; set; } // ID to sell towers
    public int X { get; set; }
    public int Y { get; set; }
    public TowerType Type { get; set; }
    // Other properties like attack power, range, etc.
}