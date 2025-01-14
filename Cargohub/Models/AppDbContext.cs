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
        public DbSet<ItemLines> Item_lines { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        // public DbSet<ItemTransfer> itemTransfers { get; set; }
        // public DbSet<OrderItem> orderItem { get; set; }
        // public DbSet<ItemShipment> itemShipment { get; set; }


    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //        base.OnModelCreating(modelBuilder);

    //         // Configure Item -> ItemLine relationship
    //         modelBuilder.Entity<Item>()
    //             .HasOne(i => i.ItemLine)
    //             .WithMany() // No navigation property in ItemLines, so we leave this empty
    //             .HasForeignKey(i => i.ItemLineId);
    //             //.OnDelete(DeleteBehavior.SetNull); // Set to null when ItemLine is deleted (soft deletion behavior)

    //         // Configure Item -> ItemGroup relationship
    //         modelBuilder.Entity<Item>()
    //             .HasOne(i => i.ItemGroup)
    //             .WithMany() // No navigation property in ItemGroup, so we leave this empty
    //             .HasForeignKey(i => i.ItemGroupId);
    //             //.OnDelete(DeleteBehavior.SetNull); // Set to null when ItemGroup is deleted (soft deletion behavior)

    //         // Configure Item -> ItemType relationship
    //         modelBuilder.Entity<Item>()
    //             .HasOne(i => i.ItemType)
    //             .WithMany() // No navigation property in ItemType, so we leave this empty
    //             .HasForeignKey(i => i.ItemTypeId);
    //             //.OnDelete(DeleteBehavior.SetNull); // Set to null when ItemType is deleted (soft deletion behavior)

    // // Set primary key for Item entity
    // modelBuilder.Entity<Item>()
    //     .HasKey(i => i.uid); // Ensure uid is the primary key for Item entity

    //         // Apply global query filters for soft deletion on all models
    //         // modelBuilder.Entity<Item>()
    //         //     .HasQueryFilter(i => !i.isdeleted);

    //         // modelBuilder.Entity<ItemLines>()
    //         //     .HasQueryFilter(il => !il.isdeleted);

    //         // modelBuilder.Entity<ItemGroup>()
    //         //     .HasQueryFilter(ig => !ig.isdeleted);

    //         // modelBuilder.Entity<ItemType>()
    //         //     .HasQueryFilter(it => !it.isdeleted);

    //         // Optional: Additional configurations for 'isdeleted' columns (if you want to ensure soft deletion behavior on insert/update)
    //         // modelBuilder.Entity<Item>()
    //         //     .Property(i => i.isdeleted)
    //         //     .HasDefaultValue(false); // Ensure default value for isdeleted column

    //         // modelBuilder.Entity<ItemLines>()
    //         //     .Property(il => il.isdeleted)
    //         //     .HasDefaultValue(false);

    //         // modelBuilder.Entity<ItemGroup>()
    //         //     .Property(ig => ig.isdeleted)
    //         //     .HasDefaultValue(false);

    //         // modelBuilder.Entity<ItemType>()
    //         //     .Property(it => it.isdeleted)
    //         //     .HasDefaultValue(false);

    //         // Optional: Index for faster queries on the 'isdeleted' column if necessary
    //         // modelBuilder.Entity<Item>()
    //         //     .HasIndex(i => i.isdeleted);

    //         // modelBuilder.Entity<ItemLines>()
    //         //     .HasIndex(il => il.isdeleted);

    //         // modelBuilder.Entity<ItemGroup>()
    //         //     .HasIndex(ig => ig.isdeleted);

    //         // modelBuilder.Entity<ItemType>()
    //         //     .HasIndex(it => it.isdeleted);

    //         // Configure Warehouse.Contact as an owned type
    //         modelBuilder.Entity<Warehouse>(entity =>
    //         {
    //             entity.OwnsOne(w => w.contact);
    //         });

    //         // Configure Shipment -> ItemShipments relationship as owned
    //         modelBuilder.Entity<Shipment>()
    //             .OwnsMany(s => s.items, item =>
    //             {
    //                 item.Property(i => i.item_id).HasColumnName("item_id");
    //                 item.Property(i => i.amount).HasColumnName("amount");
    //                 // Add other properties as necessary
    //             });

    //         // Configure Transfer -> ItemTransfers relationship as owned
    //         modelBuilder.Entity<Transfer>()
    //             .OwnsMany(t => t.items, item =>
    //             {
    //                 item.Property(i => i.item_id).HasColumnName("item_id");
    //                 item.Property(i => i.amount).HasColumnName("amount");
    //                 // Add other properties as necessary
    //             });

    //         // Configure Order -> OrderItems relationship as owned
    //         modelBuilder.Entity<Order>()
    //             .OwnsMany(o => o.items, item =>
    //             {
    //                 item.Property(i => i.item_id).HasColumnName("item_id");
    //                 item.Property(i => i.amount).HasColumnName("amount");
    //                 // Add other properties as necessary
    //             });

    //         // Configure Shipment to own items
    //         // modelBuilder.Entity<Shipment>(entity =>
    //         // {
    //         //     entity.OwnsOne(s => s.items);
    //         // });

    //         // Add any other entity configurations as needed...
            
    //         base.OnModelCreating(modelBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }

    }
}
