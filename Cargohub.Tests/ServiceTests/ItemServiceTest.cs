using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class ItemServiceTests
    {
        private AppDbContext _context;
        private ItemService _itemService;

        [TestInitialize]
        public void Setup()
        {
            // Set up an in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name for each test
                .Options;

            _context = new AppDbContext(options);

            // Seed data for ItemLines
            var itemLine = new ItemLines
            {
                id = 1,
                name = "Line 1",
                description = "Test Description",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _context.Item_lines.Add(itemLine);

            // Seed data for Items
            _context.Items.AddRange(new List<Item>
            {
                new Item
                {
                    id = 1,
                    uid = "P000001",
                    code = "CODE1",
                    description = "Test Item 1",
                    short_description = "Short Desc 1",
                    item_line = itemLine.id, // Foreign key
                    ItemLine = itemLine,    // Navigation property
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                    isdeleted = false
                },
                new Item
                {
                    id = 2,
                    uid = "P000002",
                    code = "CODE2",
                    description = "Test Item 2",
                    short_description = "Short Desc 2",
                    item_line = itemLine.id, // Foreign key
                    ItemLine = itemLine,    // Navigation property
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                    isdeleted = false
                }
            });
            _context.SaveChanges();

            _itemService = new ItemService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllItems_ReturnsCorrectAmount()
        {
            // Act
            var result = await _itemService.GetAllItems(1);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("P000001", result[0].uid);
        }

        [TestMethod]
        public async Task GetItemByUid_ReturnsCorrectItem()
        {
            // Act
            var result = await _itemService.GetItemByUid("P000001");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("P000001", result.uid);
        }

        [TestMethod]
        public async Task GetItemByUid_ReturnsNull_WhenItemDoesNotExist()
        {
            // Act
            var result = await _itemService.GetItemByUid("nonexistent");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddItemAsync_AddsItemSuccessfully()
        {
            // Arrange
            var newItem = new Item
            {
                id = 3,
                code = "CODE3",
                description = "Test Item 3",
                short_description = "Short Desc 3",
                item_line = 1, // Reference to existing ItemLine
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };

            // Act
            var result = await _itemService.AddItemAsync(newItem);

            // Assert
            Assert.IsNotNull(result.uid);
            Assert.AreEqual(3, _context.Items.Count());
        }

        [TestMethod]
        public async Task UpdateItemAsync_UpdatesExistingItemSuccessfully()
        {
            // Arrange
            var updatedItem = new Item
            {
                uid = "P000001",
                code = "NEWCODE1",
                description = "Updated Description",
                short_description = "Updated Short Desc",
                updated_at = DateTime.UtcNow
            };

            // Act
            var result = await _itemService.UpdateItemAsync("P000001", updatedItem);

            // Assert
            Assert.IsTrue(result);
            var item = _context.Items.First(i => i.uid == "P000001");
            Assert.AreEqual("NEWCODE1", item.code);
            Assert.AreEqual("Updated Description", item.description);
        }

        [TestMethod]
        public async Task UpdateItemAsync_ReturnsFalse_WhenItemDoesNotExist()
        {
            // Arrange
            var updatedItem = new Item
            {
                uid = "nonexistent",
                code = "NEWCODE1",
                description = "Updated Description",
                short_description = "Updated Short Desc",
                updated_at = DateTime.UtcNow
            };

            // Act
            var result = await _itemService.UpdateItemAsync("nonexistent", updatedItem);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task RemoveItemAsync_SoftDeletesItemSuccessfully()
        {
            // Act
            var result = await _itemService.RemoveItemAsync("P000001");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task RemoveItemAsync_ReturnsFalse_WhenItemDoesNotExist()
        {
            // Act
            var result = await _itemService.RemoveItemAsync("nonexistent");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetItemsByItemLineAsync_ReturnsCorrectItem()
        {
            // Act
            var result = await _itemService.GetItemsByItemLineAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.item_line);
        }

        [TestMethod]
        public async Task GetItemsByItemLineAsync_ReturnsNull_WhenNoItemsMatch()
        {
            // Act
            var result = await _itemService.GetItemsByItemLineAsync(999);

            // Assert
            Assert.IsNull(result);
        }
    }
}
