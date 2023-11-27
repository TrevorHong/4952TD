using System.Collections.Generic;
using System.Threading.Tasks;

public class EnemySpawner
{
    private List<Enemy> enemies = new List<Enemy>();
    private int spawnInterval = 10000; // Spawn an enemy every 10 seconds
    private int lastSpawnTime = 0;

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public async Task Update(int currentTime, List<Point> path)
    {
        if (currentTime - lastSpawnTime >= spawnInterval)
        {
            var enemy = new Enemy { Position = path[0], PathIndex = 0, Speed = 1 }; // Modify this line
            enemies.Add(enemy);
            lastSpawnTime = currentTime;
        }

        MoveEnemies(path);
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

public void CheckCollisions(List<Tower> towers)
{
    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        var enemy = enemies[i];
        foreach (var tower in towers)
        {
            var dx = tower.X - enemy.Position.X;
            var dy = tower.Y - enemy.Position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 50) // Assuming the size of the tower and enemy is 50
            {
                enemies.RemoveAt(i);
                break;
            }
        }
    }
}

}