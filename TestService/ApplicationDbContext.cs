using Microsoft.EntityFrameworkCore;
using TestService.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserData> UserData { get; set; } 
}
