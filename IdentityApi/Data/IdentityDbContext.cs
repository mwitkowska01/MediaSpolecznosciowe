using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
