using BlazorTD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService
{

    private int? towerBought;

    private int? totalGold;

    public int? enemy;

    public string? Username { get; set; }

    private readonly ApplicationDbContext _context;

    private readonly object UpdateLock = new object();

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public int? GoldSpent {
        get => totalGold ?? 0;
        set
        {
            totalGold = value;
            lock (UpdateLock)
            {
                _ = UpdateGoldSpentInDatabaseAsync();
                _ = UpdateTowerBoughtInDatabaseAsync();
                _ = UpdateEnemyCounterInDatabaseAsync();
            }
        }
      }

    public int? TowerBought {
        get => towerBought ?? 0;
        set
        {
            towerBought = value;
            lock (UpdateLock)
            {
                _ = UpdateGoldSpentInDatabaseAsync();
                _ = UpdateTowerBoughtInDatabaseAsync();
                _ = UpdateEnemyCounterInDatabaseAsync();
            }
        }
    }

    public int? EnemyCounter {
        get => enemy ?? 0;
        set
        {
            enemy = value;
            lock (UpdateLock)
            {
                _ = UpdateGoldSpentInDatabaseAsync();
                _ = UpdateTowerBoughtInDatabaseAsync();
                _ = UpdateEnemyCounterInDatabaseAsync();
            }
        }
    }

// Updates the GoldSpent in the database
private async Task UpdateGoldSpentInDatabaseAsync()
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == Username);
    if (user != null)
    {
        user.GoldSpent = totalGold; // Set GoldSpent
        _context.Users.Update(user); // Track changes
        await _context.SaveChangesAsync(); // Save changes
    }
}

// Updates the TowerBought in the database
private async Task UpdateTowerBoughtInDatabaseAsync()
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == Username);
    if (user != null)
    {
        user.TowerBought = towerBought; // Set TowerBought
        _context.Users.Update(user); // Track changes
        await _context.SaveChangesAsync(); // Save changes
    }
}

// Updates the EnemyCounter in the database
private async Task UpdateEnemyCounterInDatabaseAsync()
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == Username);
    if (user != null)
    {
        user.EnemyCounter = enemy; // Set EnemyCounter
        _context.Users.Update(user); // Track changes
        await _context.SaveChangesAsync(); // Save changes
    }
}
}