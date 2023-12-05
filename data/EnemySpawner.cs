using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorServerApp.Pages;
public partial class EnemySpawner
{
    private List<Enemy> enemies = new List<Enemy>();
    private int spawnInterval = 5000; // Spawn an enemy every 5 seconds
    private int lastSpawnTime = 0;

    public int gameState = 0;
    float speed = 2.50f;

    private int? health;

    private int? gold;

    public int? enemy;

    private readonly UserService _userService;

    public EnemySpawner(UserService userService)
    {
        _userService = userService;
    }
    public int Currency {
        get => gold ?? 250;
        set => gold = value;
     }
    public int enemyCounter
    {
        get => enemy ?? 0;
        set
        {
            enemy = value;
            _userService.EnemyCounter = _userService.EnemyCounter + enemyCounter;
        }
    }

    public int? TotalHealth {
        get => health ?? 100;
        set {

            if(value.HasValue) {
                health = Math.Max(value.Value, 0);
            }
        }
    }
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public List<Enemy> Update(int currentTime, List<Point> path)
    {
        if (currentTime - lastSpawnTime >= spawnInterval)
        {
            var enemy = new Enemy { Position = path[0], PathIndex = 0, Speed = speed }; // Modify this line
            enemies.Add(enemy);
            lastSpawnTime = currentTime;
        }

        if(gameState != 0) {
            MoveEnemies(path);
        } else {
            enemies.Clear();
        }

        return enemies;
    }

    private void MoveEnemies(List<Point> path)
{
    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        var enemy = enemies[i];
        if (enemy.PathIndex < path.Count)
        {
            var nextPosition = path[enemy.PathIndex];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var dx = nextPosition.X - enemy.Position.X;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                var dy = nextPosition.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance <= enemy.Speed)
            {
                enemy.PathIndex++;
                if (enemy.PathIndex < path.Count)
                {
                    enemy.Position = path[enemy.PathIndex];
                }
                else
                {
                    TotalHealth = TotalHealth - 100;
                    enemies.RemoveAt(i); // Remove the enemy when it reaches the last point
                    
                }
            }
            else
            {
                enemy.Position.X += (int)(enemy.Speed * dx / distance);
                enemy.Position.Y += (int)(enemy.Speed * dy / distance);
            }
        }
    }
}


public void CheckMeleeCollisions(List<MeleeTower> meleeTowers)
{
    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        var enemy = enemies[i];
        foreach (var tower in meleeTowers)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var dx = tower.X - enemy.Position.X;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                var dy = tower.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 50) // Assuming the size of the tower and enemy is 50
            {
                enemies.RemoveAt(i);
                enemyCounter += 1;
                Currency += 5;
                break;
            }
        }
    }
}

public void CheckBombCollisions(List<BombTower> bombTowers)
{
    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        var enemy = enemies[i];
        for (int j = bombTowers.Count - 1; j >= 0; j--)
        {
            var tower = bombTowers[j];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var dx = tower.X - enemy.Position.X;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                var dy = tower.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 50) // Assuming the size of the tower and enemy is 50
            {
                enemies.RemoveAt(i);
                Currency += 5;
                bombTowers.RemoveAt(j);
                break;
            }
        }
    }
}

public void CheckStunCollisions(List<StunTower> stunTowers, int currentTime)
{
    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        var enemy = enemies[i];
        foreach (var tower in stunTowers)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var dx = tower.X - enemy.Position.X;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                var dy = tower.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 50) // Assuming the size of the tower and enemy is 50
            {
                enemy.Stunned = true;
                enemy.StunEndTime = currentTime + 20000; // 3 seconds from now
                break;
            }
        }
    }
}

}