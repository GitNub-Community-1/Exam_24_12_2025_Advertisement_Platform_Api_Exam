using Domain.Models.Entity;
using Infastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public Seeder(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> SeedRole()
    {
        var newroles = new List<IdentityRole<long>>()
        {
            new IdentityRole<long>(Roles.Admin),
            new IdentityRole<long>(Roles.Moderator),
            new IdentityRole<long>(Roles.User)
        };

        var existing = _roleManager.Roles.ToList();
        foreach (var role in newroles)
        {
            if (existing.Exists(e => e.Name == role.Name) == false)
            {
                await _roleManager.CreateAsync(role);
            }
        }

        return true;

    }

    public async Task<bool> SeedUser()
    {
        var existing = await _userManager.FindByNameAsync("admin");
        if (existing != null) return false;
        
        var identity = new User()
        {
            UserName = "admin",
            PhoneNumber = "13456777",
            Email = "admin@gmail.com",
            FirstName = "Admin",
            LastName = "User"
        };

        var result = await _userManager.CreateAsync(identity, "hello123");
        await _userManager.AddToRoleAsync(identity, Roles.Admin);
        return result.Succeeded;
    }
    
   

}

public class Roles
{
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";
    public const string User = "User";
}