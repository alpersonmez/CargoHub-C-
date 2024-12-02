using Cargohub.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Cargohub.Data
{
    public static class ItemSeeder
    {
        public static void SeedDatabase(AppDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed ItemTypes if not already present
            if (!context.ItemTypes.Any())
            {
                context.ItemTypes.AddRange(
                    new ItemType
                    {
                        Name = "Electronics",
                        Description = "Items related to electronic devices",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new ItemType
                    {
                        Name = "Furniture",
                        Description = "Various furniture items",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new ItemType
                    {
                        Name = "Groceries",
                        Description = "Daily grocery items",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }

            // Seed Items if not already present
            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item
                    {
                        Uid = "P001",
                        Code = "ITEM001",
                        Description = "Test item 1 description",
                        ShortDescription = "Test item 1 short description",
                        UpcCode = "1234567890",
                        ModelNumber = "M-001",
                        CommodityCode = "C-001",
                        ItemLine = 1,
                        ItemGroup = 1,
                        ItemType = 1, // Links to Electronics
                        UnitPurchaseQuantity = 10,
                        UnitOrderQuantity = 5,
                        PackOrderQuantity = 2,
                        SupplierId = 1,
                        SupplierCode = "SUP1",
                        SupplierPartNumber = "PART-001",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Item
                    {
                        Uid = "P002",
                        Code = "ITEM002",
                        Description = "Test item 2 description",
                        ShortDescription = "Test item 2 short description",
                        UpcCode = "0987654321",
                        ModelNumber = "M-002",
                        CommodityCode = "C-002",
                        ItemLine = 2,
                        ItemGroup = 2,
                        ItemType = 2, // Links to Furniture
                        UnitPurchaseQuantity = 20,
                        UnitOrderQuantity = 10,
                        PackOrderQuantity = 5,
                        SupplierId = 2,
                        SupplierCode = "SUP2",
                        SupplierPartNumber = "PART-002",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Item
                    {
                        Uid = "P003",
                        Code = "ITEM003",
                        Description = "Test item 3 description",
                        ShortDescription = "Test item 3 short description",
                        UpcCode = "1122334455",
                        ModelNumber = "M-003",
                        CommodityCode = "C-003",
                        ItemLine = 3,
                        ItemGroup = 3,
                        ItemType = 3, // Links to Groceries
                        UnitPurchaseQuantity = 30,
                        UnitOrderQuantity = 15,
                        PackOrderQuantity = 7,
                        SupplierId = 3,
                        SupplierCode = "SUP3",
                        SupplierPartNumber = "PART-003",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
