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
    public class ItemLinesServiceTests
    {
        private AppDbContext _context;
        private ItemLinesService _itemLinesService;

        [TestInitialize]
        public void Setup()
        {
            // Set up an in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _itemLinesService = new ItemLinesService(_context);

            // Seed the in-memory database
            _context.Item_lines.AddRange(new List<ItemLines>
            {
                new ItemLines
                {
                    id = 1,
                    name = "Line 1",
                    description = "Description 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemLines
                {
                    id = 2,
                    name = "Line 2",
                    description = "Description 2",
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
        public async Task GetAllItemLines_ReturnsCorrectAmount()
        {
            // Act
            var itemLines = await _itemLinesService.GetAllItemLines(2);

            // Assert
            Assert.AreEqual(2, itemLines.Count);
        }

        [TestMethod]
        public async Task GetItemLineById_ReturnsCorrectItemLine()
        {
            // Act
            var itemLine = await _itemLinesService.GetItemLineById(1);

            // Assert
            Assert.IsNotNull(itemLine);
            Assert.AreEqual("Line 1", itemLine.name);
            Assert.AreEqual("Description 1", itemLine.description);
        }

        [TestMethod]
        public async Task GetItemLineById_ReturnsNull_WhenItemLineDoesNotExist()
        {
            // Act
            var itemLine = await _itemLinesService.GetItemLineById(99);

            // Assert
            Assert.IsNull(itemLine);
        }

        [TestMethod]
        public async Task UpdateItemLine_UpdatesExistingItemLineSuccessfully()
        {
            // Arrange
            var existingItemLine = await _itemLinesService.GetItemLineById(1);
            existingItemLine.description = "Updated Description 1";

            // Act
            var updated = await _itemLinesService.UpdateItemLine(existingItemLine);

            // Assert
            Assert.IsTrue(updated);
            var updatedItemLine = await _itemLinesService.GetItemLineById(1);
            Assert.AreEqual("Updated Description 1", updatedItemLine.description);
        }

        [TestMethod]
        public async Task UpdateItemLine_ReturnsFalse_WhenItemLineDoesNotExist()
        {
            // Arrange
            var nonExistingItemLine = new ItemLines
            {
                id = 99,
                name = "Nonexistent Line",
                description = "Nonexistent Description"
            };

            // Act
            var updated = await _itemLinesService.UpdateItemLine(nonExistingItemLine);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteItemLine_SoftDeletesItemLineSuccessfully()
        {
            // Act
            var deleted = await _itemLinesService.DeleteItemLine(1);

            // Assert
            Assert.IsTrue(deleted);
            var itemLine = await _itemLinesService.GetItemLineById(1);
            Assert.IsTrue(itemLine.isdeleted);
        }

        [TestMethod]
        public async Task DeleteItemLine_ReturnsFalse_WhenItemLineDoesNotExist()
        {
            // Act
            var deleted = await _itemLinesService.DeleteItemLine(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
