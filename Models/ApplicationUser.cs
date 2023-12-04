using Microsoft.AspNetCore.Identity;

namespace BlazorTD.Models;
public class ApplicationUser : IdentityUser
{

    public int? GoldSpent { get; set; }

    public int? TowerBought { get; set; }

    public int? EnemyCounter { get; set; }
    // No additional properties needed
}