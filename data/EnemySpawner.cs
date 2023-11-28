using System.Collections.Generic;
using System.Threading.Tasks;

public class EnemySpawner
{
    private List<Enemy> enemies = new List<Enemy>();
    private int spawnInterval = 5000; // Spawn an enemy every 5 seconds
    private int lastSpawnTime = 0;

    float speed = 2.50f;

    public int Currency { get; set; }

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

        MoveEnemies(path);

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
            var dx = nextPosition.X - enemy.Position.X;
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
            var dx = tower.X - enemy.Position.X;
            var dy = tower.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 50) // Assuming the size of the tower and enemy is 50
            {
                enemies.RemoveAt(i);
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
            var dx = tower.X - enemy.Position.X;
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
            var dx = tower.X - enemy.Position.X;
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