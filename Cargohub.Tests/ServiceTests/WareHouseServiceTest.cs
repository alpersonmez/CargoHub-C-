using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class WarehouseServiceTests
    {
        private AppDbContext _context;
        private WarehouseService _warehouseService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _warehouseService = new WarehouseService(_context);

            // Seed the in-memory database
            _context.Warehouses.AddRange(new List<Warehouse>
            {
                new Warehouse
                {
                    id = 1,
                    code = "WH1",
                    name = "Warehouse 1",
                    address = "123 Street",
                    zip = "12345",
                    city = "City A",
                    province = "Province A",
                    country = "Country A",
                    contact = new Warehouse.Contact
                    {
                        name = "John Doe",
                        phone = "555-1234",
                        email = "john.doe@example.com"
                    },
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Warehouse
                {
                    id = 2,
                    code = "WH2",
                    name = "Warehouse 2",
                    address = "456 Avenue",
                    zip = "67890",
                    city = "City B",
                    province = "Province B",
                    country = "Country B",
                    contact = new Warehouse.Contact
                    {
                        name = "Jane Smith",
                        phone = "555-5678",
                        email = "jane.smith@example.com"
                    },
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            });
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllWarehouses_ReturnsCorrectAmount()
        {
            // Act
            var warehouses = await _warehouseService.GetAllWarehouses(2);

            // Assert
            Assert.AreEqual(2, warehouses.Count);
        }

        [TestMethod]
        public async Task GetWarehouseById_ReturnsCorrectWarehouse()
        {
            // Act
            var warehouse = await _warehouseService.GetWarehouseById(1);

            // Assert
            Assert.IsNotNull(warehouse);
            Assert.AreEqual("WH1", warehouse.code);
            Assert.AreEqual("John Doe", warehouse.contact.name);
        }

        [TestMethod]
        public async Task GetWarehouseById_ReturnsNull_WhenWarehouseDoesNotExist()
        {
            // Act
            var warehouse = await _warehouseService.GetWarehouseById(99);

            // Assert
            Assert.IsNull(warehouse);
        }

        [TestMethod]
        public async Task AddWarehouse_AddsWarehouseSuccessfully()
        {
            // Arrange
            var newWarehouse = new Warehouse
            {
                code = "WH3",
                name = "Warehouse 3",
                address = "789 Road",
                zip = "11223",
                city = "City C",
                province = "Province C",
                country = "Country C",
                contact = new Warehouse.Contact
                {
                    name = "Alice Johnson",
                    phone = "555-9999",
                    email = "alice.johnson@example.com"
                }
            };

            // Act
            var addedWarehouse = await _warehouseService.AddWarehouse(newWarehouse);

            // Assert
            Assert.IsNotNull(addedWarehouse);
            Assert.AreEqual("WH3", addedWarehouse.code);
            Assert.AreEqual("Alice Johnson", addedWarehouse.contact.name);
            Assert.AreEqual(3, _context.Warehouses.Count());
        }

        [TestMethod]
        public async Task UpdateWarehouse_UpdatesExistingWarehouseSuccessfully()
        {
            // Arrange
            var existingWarehouse = await _warehouseService.GetWarehouseById(1);
            existingWarehouse.name = "Updated Warehouse 1";
            existingWarehouse.contact.phone = "555-0000";

            // Act
            var updated = await _warehouseService.UpdateWarehouse(existingWarehouse);

            // Assert
            Assert.IsTrue(updated);
            var updatedWarehouse = await _warehouseService.GetWarehouseById(1);
            Assert.AreEqual("Updated Warehouse 1", updatedWarehouse.name);
            Assert.AreEqual("555-0000", updatedWarehouse.contact.phone);
        }

        [TestMethod]
        public async Task UpdateWarehouse_ReturnsFalse_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var nonExistingWarehouse = new Warehouse
            {
                id = 99,
                code = "WH99",
                name = "Nonexistent Warehouse",
                contact = new Warehouse.Contact
                {
                    name = "Ghost",
                    phone = "555-ghost",
                    email = "ghost@example.com"
                }
            };

            // Act
            var updated = await _warehouseService.UpdateWarehouse(nonExistingWarehouse);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteWarehouse_SoftDeletesWarehouseSuccessfully()
        {
            // Act
            var deleted = await _warehouseService.DeleteWarehouse(1);

            // Assert
            Assert.IsTrue(deleted);
            var warehouse = await _warehouseService.GetWarehouseById(1);
            Assert.IsTrue(warehouse.isdeleted);
        }

        [TestMethod]
        public async Task DeleteWarehouse_ReturnsFalse_WhenWarehouseDoesNotExist()
        {
            // Act
            var deleted = await _warehouseService.DeleteWarehouse(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
