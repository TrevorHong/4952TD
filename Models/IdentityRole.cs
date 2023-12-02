using Microsoft.AspNetCore.Identity;

namespace BlazorTD.Models;
public class ApplicationRole : IdentityRole
{
    public int Score { get; set; }
    // Add additional properties here as needed...
}