public class UserService
{

    private int? towerBought;
    public string? Username { get; set; }

    public int? EnemiesKilled { get; set; }

    public int? GoldSpent { get; set; }

    public int? TowerBought {
        get => towerBought ?? 0;
        set => towerBought = value;
    }
}