using Microsoft.AspNetCore.Identity;

namespace BlazorTD.Models;
public class ApplicationUser : IdentityUser
{

    public int? GoldSpent { get; set; }
    // No additional properties needed
}