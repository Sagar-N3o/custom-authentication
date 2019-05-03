using CustomTokenBasedAuthentication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomTokenBasedAuthentication.Database.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
