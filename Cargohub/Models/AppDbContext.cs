using Cargohub.Models;
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
        public DbSet<Dock> Docks { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<ItemLines> Item_lines { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ImportStatus> ImportStatuses { get; set; }
        public DbSet<OrderShipment> OrderShipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderShipment>()
                    .HasOne(os => os.Order)
                    .WithMany(o => o.OrderShipments)
                    .HasForeignKey(os => os.order_id);

                modelBuilder.Entity<OrderShipment>()
                    .HasOne(os => os.Shipment)
                    .WithMany(s => s.OrderShipments)
                    .HasForeignKey(os => os.shipment_id);

            // Configure Stock hierarchy with discriminator
            modelBuilder.Entity<Warehouse>()
                .OwnsOne(w => w.contact, contact =>
                {
                    contact.Property(c => c.name).HasColumnName("contact_name");
                    contact.Property(c => c.phone).HasColumnName("contact_phone");
                    contact.Property(c => c.email).HasColumnName("contact_email");
                });

            // Configure Stock hierarchy with discriminator
            modelBuilder.Entity<Stock>()
                .ToTable("Stocks")
                .HasDiscriminator<string>("StockType")
                .HasValue<OrderStock>("Order")
                .HasValue<ShipmentStock>("Shipment")
                .HasValue<TransferStock>("Transfer");

            modelBuilder.Entity<Dock>()
                .HasOne(d => d.warehouse)
                .WithMany(w => w.docks)
                .HasForeignKey(d => d.warehouse_id);

            modelBuilder.Entity<Dock>()
                .HasIndex(d => d.code)
                .IsUnique();

            // Apply soft-delete query filters
            modelBuilder.Entity<Client>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Inventory>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Item>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<ItemGroup>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<ItemLines>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<ItemType>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Location>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Shipment>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Supplier>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Transfer>().HasQueryFilter(e => !e.isdeleted);
            modelBuilder.Entity<Warehouse>().HasQueryFilter(e => !e.isdeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}