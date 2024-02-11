using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi
{
    public class UserAccountsDbContext: DbContext
    {
        public UserAccountsDbContext(DbContextOptions<UserAccountsDbContext> options)
      : base(options)
        {
        }

        public DbSet<UserLogin> UserLogin { get; set; }
    }
}
