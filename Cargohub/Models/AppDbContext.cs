using Cargohub.Services;
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
                public DbSet<Item_lines> Item_lines { get; set; }
                public DbSet<ItemGroup> ItemGroups { get; set; }
                public DbSet<Supplier> Supplier { get; set; }
                public DbSet<Shipment> Shipments { get; set; }
                public DbSet<Transfer> Transfers { get; set; }
                public DbSet<Client> Clients { get; set; }

        }
}

