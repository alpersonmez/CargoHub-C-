using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Cargohub.Tests
{
    [TestClass]
    public class ItemGroupServiceTests
    {
        private AppDbContext _context;
        private ItemGroupService _itemGroupService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _itemGroupService = new ItemGroupService(_context);

            // Seed the in-memory database
            _context.ItemGroups.AddRange(new List<ItemGroup>
            {
                new ItemGroup
                {
                    id = 1,
                    name = "Group 1",
                    description = "Test Group 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemGroup
                {
                    id = 2,
                    name = "Group 2",
                    description = "Test Group 2",
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
        public async Task GetAllItemGroups_ReturnsCorrectAmount()
        {
            // Act
            var itemGroups = await _itemGroupService.GetAllItemGroups(2);

            // Assert
            Assert.AreEqual(2, itemGroups.Count);
        }

        [TestMethod]
        public async Task GetItemGroupById_ReturnsCorrectItemGroup()
        {
            // Act
            var itemGroup = await _itemGroupService.GetItemGroupById(1);

            // Assert
            Assert.IsNotNull(itemGroup);
            Assert.AreEqual("Group 1", itemGroup.name);
            Assert.AreEqual("Test Group 1", itemGroup.description);
        }

        [TestMethod]
        public async Task GetItemGroupById_ReturnsNull_WhenItemGroupDoesNotExist()
        {
            // Act
            var itemGroup = await _itemGroupService.GetItemGroupById(99);

            // Assert
            Assert.IsNull(itemGroup);
        }

        [TestMethod]
        public async Task UpdateItem_Groups_UpdatesExistingItemGroupSuccessfully()
        {
            // Arrange
            var existingItemGroup = await _itemGroupService.GetItemGroupById(1);
            existingItemGroup.name = "Updated Group 1";

            // Act
            var updated = await _itemGroupService.UpdateItem_Groups(existingItemGroup);

            // Assert
            Assert.IsTrue(updated);
            var updatedItemGroup = await _itemGroupService.GetItemGroupById(1);
            Assert.AreEqual("Updated Group 1", updatedItemGroup.name);
        }

        [TestMethod]
        public async Task UpdateItem_Groups_ReturnsFalse_WhenItemGroupDoesNotExist()
        {
            // Arrange
            var nonExistingItemGroup = new ItemGroup
            {
                id = 99,
                name = "Nonexistent Group",
                description = "Nonexistent Description"
            };

            // Act
            var updated = await _itemGroupService.UpdateItem_Groups(nonExistingItemGroup);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteItem_Groups_SoftDeletesItemGroupSuccessfully()
        {
            // Act
            var deleted = await _itemGroupService.DeleteItem_Groups(1);

            // Assert
            Assert.IsTrue(deleted);
            var itemGroup = await _itemGroupService.GetItemGroupById(1);
            Assert.IsTrue(itemGroup.isdeleted);
        }

        [TestMethod]
        public async Task DeleteItem_Groups_ReturnsFalse_WhenItemGroupDoesNotExist()
        {
            // Act
            var deleted = await _itemGroupService.DeleteItem_Groups(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
