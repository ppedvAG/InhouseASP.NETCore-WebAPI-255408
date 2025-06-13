using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Models;

public class AppUser : IdentityUser
{
    public string? FavoriteFood { get; set; }
}
