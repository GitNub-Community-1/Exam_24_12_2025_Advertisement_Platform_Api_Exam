using Domain.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<User, IdentityRole<long>, long>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Advertisement>  Advertisements { get; set; }
    public DbSet<Category> Categories { get; set; }
}