using Microsoft.EntityFrameworkCore;

namespace Cargohub.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        // Add DbSet properties for other entities as needed
    }
}

