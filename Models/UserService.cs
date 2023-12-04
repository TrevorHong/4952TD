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

    private readonly object lockObject = new object();

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public int? GoldSpent {
        get => totalGold ?? 0;
        set
        {
            totalGold = value;
            lock (lockObject)
            {
                _ = UpdateGoldSpentInDatabaseAsync();
            }
        }
      }

    public int? TowerBought {
        get => towerBought ?? 0;
        set => towerBought = value;
    }

    public int? enemyCounter {
        get => enemy ?? 0;
        set => enemy = value;
    }

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
}