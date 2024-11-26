using Microsoft.EntityFrameworkCore;

namespace Cargohub.Models
{
        public class AppDbContext : DbContext
        {
                public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

                public DbSet<Order> Orders { get; set; }
                public DbSet<Item> Items { get; set; }
                public DbSet<Location> Locations { get; set; }
                public DbSet<Warehouse> Warehouses { get; set; }
                public DbSet<ItemType> ItemTypes { get; set; }
        }
}

