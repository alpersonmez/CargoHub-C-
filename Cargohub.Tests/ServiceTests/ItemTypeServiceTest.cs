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
    public class ItemTypeServiceTests
    {
        private AppDbContext _context;
        private ItemTypeService _itemTypeService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _context.ItemTypes.AddRange(new List<ItemType>
            {
                new ItemType
                {
                    id = 1,
                    name = "ItemType 1",
                    description = "Description 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemType
                {
                    id = 2,
                    name = "ItemType 2",
                    description = "Description 2",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            });
            _context.SaveChanges();

            _itemTypeService = new ItemTypeService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllItemTypes_ReturnsCorrectAmount()
        {
            // Act
            var result = await _itemTypeService.GetAllItemTypes(2);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetItemTypeById_ReturnsCorrectItemType()
        {
            // Act
            var result = await _itemTypeService.GetItemTypeById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ItemType 1", result.name);
            Assert.AreEqual("Description 1", result.description);
        }

        [TestMethod]
        public async Task GetItemTypeById_ReturnsNull_WhenItemTypeDoesNotExist()
        {
            // Act
            var result = await _itemTypeService.GetItemTypeById(99);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateItemType_UpdatesExistingItemTypeSuccessfully()
        {
            // Arrange
            var updatedItemType = new ItemType
            {
                id = 1,
                name = "Updated ItemType",
                description = "Updated Description",
                updated_at = DateTime.UtcNow
            };

            // Act
            var result = await _itemTypeService.UpdateItemType(updatedItemType);

            // Assert
            Assert.IsTrue(result);
            var itemType = await _itemTypeService.GetItemTypeById(1);
            Assert.AreEqual("Updated ItemType", itemType.name);
            Assert.AreEqual("Updated Description", itemType.description);
        }

        [TestMethod]
        public async Task UpdateItemType_ReturnsFalse_WhenItemTypeDoesNotExist()
        {
            // Arrange
            var nonExistingItemType = new ItemType
            {
                id = 99,
                name = "Nonexistent ItemType",
                description = "Nonexistent Description"
            };

            // Act
            var result = await _itemTypeService.UpdateItemType(nonExistingItemType);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteItemType_SoftDeletesItemTypeSuccessfully()
        {
            // Act
            var result = await _itemTypeService.DeleteItemType(1);

            // Assert
            Assert.IsTrue(result);
            var itemType = await _itemTypeService.GetItemTypeById(1);
            Assert.IsTrue(itemType.isdeleted);
        }

        [TestMethod]
        public async Task DeleteItemType_ReturnsFalse_WhenItemTypeDoesNotExist()
        {
            // Act
            var result = await _itemTypeService.DeleteItemType(99);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
