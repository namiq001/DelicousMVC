using Microsoft.AspNetCore.Identity;

namespace DelicousMVC.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}
