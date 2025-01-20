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
    public class InventoryServiceTests
    {
        private AppDbContext _context;
        private InventoryService _inventoryService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _inventoryService = new InventoryService(_context);

            // Seed data
            _context.Inventories.AddRange(new List<Inventory>
            {
                new Inventory
                {
                    id = 1,
                    item_id = "item001",
                    description = "Inventory Item 1",
                    item_reference = "REF001",
                    total_on_hand = 50,
                    total_expected = 10,
                    total_ordered = 20,
                    total_allocated = 5,
                    total_available = 45,
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Inventory
                {
                    id = 2,
                    item_id = "item002",
                    description = "Inventory Item 2",
                    item_reference = "REF002",
                    total_on_hand = 100,
                    total_expected = 30,
                    total_ordered = 40,
                    total_allocated = 10,
                    total_available = 90,
                    created_at = DateTime.UtcNow.AddDays(-15),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            });

            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllInventories_ReturnsCorrectAmount()
        {
            // Act
            var inventories = await _inventoryService.GetAllInventories(2);

            // Assert
            Assert.AreEqual(2, inventories.Count);
        }

        [TestMethod]
        public async Task GetInventoryById_ReturnsCorrectInventory()
        {
            // Act
            var inventory = await _inventoryService.GetInventoryById(1);

            // Assert
            Assert.IsNotNull(inventory);
            Assert.AreEqual("Inventory Item 1", inventory.description);
            Assert.AreEqual("item001", inventory.item_id);
        }

        [TestMethod]
        public async Task GetInventoryById_ReturnsNull_WhenInventoryDoesNotExist()
        {
            // Act
            var inventory = await _inventoryService.GetInventoryById(99);

            // Assert
            Assert.IsNull(inventory);
        }

        [TestMethod]
        public async Task AddInventory_AddsInventorySuccessfully()
        {
            // Arrange
            var newInventory = new Inventory
            {
                item_id = "item003",
                description = "New Inventory Item",
                item_reference = "REF003",
                total_on_hand = 60,
                total_expected = 20,
                total_ordered = 30,
                total_allocated = 10,
                total_available = 50
            };

            // Act
            var addedInventory = await _inventoryService.AddInventory(newInventory);

            // Assert
            Assert.IsNotNull(addedInventory);
            Assert.AreEqual("New Inventory Item", addedInventory.description);
            Assert.AreEqual(3, _context.Inventories.Count());
        }

        [TestMethod]
        public async Task UpdateInventory_UpdatesExistingInventorySuccessfully()
        {
            // Arrange
            var existingInventory = await _inventoryService.GetInventoryById(1);
            existingInventory.description = "Updated Inventory Description";

            // Act
            var updated = await _inventoryService.UpdateInventory(existingInventory);

            // Assert
            Assert.IsTrue(updated);
            var updatedInventory = await _inventoryService.GetInventoryById(1);
            Assert.AreEqual("Updated Inventory Description", updatedInventory.description);
        }

        [TestMethod]
        public async Task UpdateInventory_ReturnsFalse_WhenInventoryDoesNotExist()
        {
            // Arrange
            var nonExistingInventory = new Inventory
            {
                id = 99,
                description = "Nonexistent Inventory"
            };

            // Act
            var updated = await _inventoryService.UpdateInventory(nonExistingInventory);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteInventory_SoftDeletesInventorySuccessfully()
        {
            // Act
            var deleted = await _inventoryService.DeleteInventory(1);

            // Assert
            Assert.IsTrue(deleted);
            var inventory = await _inventoryService.GetInventoryById(1);
            Assert.IsTrue(inventory.isdeleted);
        }

        [TestMethod]
        public async Task DeleteInventory_ReturnsFalse_WhenInventoryDoesNotExist()
        {
            // Act
            var deleted = await _inventoryService.DeleteInventory(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
