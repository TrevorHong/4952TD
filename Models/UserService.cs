public class UserService
{

    private int? towerBought;

    private int? totalGold;
    public string? Username { get; set; }

    public int? EnemiesKilled { get; set; }

    public int? GoldSpent {
        get => totalGold ?? 0;
        set => totalGold = value;
      }

    public int? TowerBought {
        get => towerBought ?? 0;
        set => towerBought = value;
    }
}