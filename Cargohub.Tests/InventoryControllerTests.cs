using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Controllers;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class InventoryControllerTests
    {
        private Mock<IInventoryService> _mockInventoryService;
        private InventoryController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockInventoryService = new Mock<IInventoryService>();
            _controller = new InventoryController(_mockInventoryService.Object);
        }

        [TestMethod]
        public async Task GetAllInventories_ReturnsOkResult_WithListOfInventories()
        {
            // Arrange
            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    id = 1,
                    item_id = "ITEM001",
                    description = "Inventory item 1",
                    item_reference = "REF001",
                    locations = new List<int> { 1, 2 },
                    total_on_hand = 100,
                    total_expected = 50,
                    total_ordered = 30,
                    total_allocated = 20,
                    total_available = 80,
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Inventory
                {
                    id = 2,
                    item_id = "ITEM002",
                    description = "Inventory item 2",
                    item_reference = "REF002",
                    locations = new List<int> { 3 },
                    total_on_hand = 200,
                    total_expected = 100,
                    total_ordered = 50,
                    total_allocated = 30,
                    total_available = 150,
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            };
            _mockInventoryService.Setup(service => service.GetAllInventories(It.IsAny<int>())).ReturnsAsync(inventories);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(inventories, okResult.Value);
        }

        [TestMethod]
        public async Task GetInventoryById_ReturnsOkResult_WithInventory()
        {
            // Arrange
            var inventory = new Inventory
            {
                id = 1,
                item_id = "ITEM001",
                description = "Inventory item 1",
                item_reference = "REF001",
                locations = new List<int> { 1, 2 },
                total_on_hand = 100,
                total_expected = 50,
                total_ordered = 30,
                total_allocated = 20,
                total_available = 80,
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockInventoryService.Setup(service => service.GetInventoryById(1)).ReturnsAsync(inventory);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(inventory, okResult.Value);
        }

        [TestMethod]
        public async Task GetInventoryById_ReturnsNotFound_WhenInventoryDoesNotExist()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.GetInventoryById(It.IsAny<int>())).ReturnsAsync((Inventory)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateInventory_ReturnsCreatedAtActionResult_WithCreatedInventory()
        {
            // Arrange
            var inventory = new Inventory
            {
                id = 1,
                item_id = "ITEM001",
                description = "Inventory item 1",
                item_reference = "REF001",
                locations = new List<int> { 1, 2 },
                total_on_hand = 100,
                total_expected = 50,
                total_ordered = 30,
                total_allocated = 20,
                total_available = 80,
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockInventoryService.Setup(service => service.AddInventory(inventory)).ReturnsAsync(inventory);

            // Act
            var result = await _controller.Create(inventory);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(inventory, createdResult.Value);
        }

        [TestMethod]
        public async Task UpdateInventory_ReturnsNoContentResult_WhenInventoryIsUpdated()
        {
            // Arrange
            var inventory = new Inventory
            {
                id = 1,
                item_id = "ITEM001",
                description = "Inventory item 1",
                item_reference = "REF001",
                locations = new List<int> { 1, 2 },
                total_on_hand = 100,
                total_expected = 50,
                total_ordered = 30,
                total_allocated = 20,
                total_available = 80,
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockInventoryService.Setup(service => service.UpdateInventory(inventory)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, inventory);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UpdateInventory_ReturnsNotFound_WhenInventoryDoesNotExist()
        {
            // Arrange
            var inventory = new Inventory
            {
                id = 1,
                item_id = "ITEM001",
                description = "Inventory item 1",
                item_reference = "REF001",
                locations = new List<int> { 1, 2 },
                total_on_hand = 100,
                total_expected = 50,
                total_ordered = 30,
                total_allocated = 20,
                total_available = 80,
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockInventoryService.Setup(service => service.UpdateInventory(inventory)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, inventory);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteInventory_ReturnsNoContentResult_WhenInventoryIsDeleted()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.DeleteInventory(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteInventory_ReturnsNotFound_WhenInventoryDoesNotExist()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.DeleteInventory(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
