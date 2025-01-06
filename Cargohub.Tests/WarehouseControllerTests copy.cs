using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Controllers;
using Cargohub.Models;
using Cargohub.Services;
using System.Collections.Generic;


namespace Cargohub.Tests
{
    [TestClass]
    public class WarehouseControllerTests
    {
        private Mock<IWarehouseService> _mockWarehouseService;
        private WarehouseController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockWarehouseService = new Mock<IWarehouseService>();
            _controller = new WarehouseController(_mockWarehouseService.Object);
        }

        // Test GetAllWarehouses
        [TestMethod]
        public async Task GetAllWarehouses_ReturnsOkResult_WithListOfWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse>
            {
                new Warehouse{
                    id = 1,
                    name = "Waardehuis"
                },
                new Warehouse{
                    id = 2,
                    name = "Garage"
                }
            };
            _mockWarehouseService.Setup(service => service.GetAllWarehouses(100)).ReturnsAsync(warehouses);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(warehouses, okResult.Value);
        }

        // Test GetWarehouseById - Success
        [TestMethod]
        public async Task GetWarehouseById_ReturnsOkResult_WithWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                name = "Waardehuis"
            };
            _mockWarehouseService.Setup(service => service.GetWarehouseById(1)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(warehouse, okResult.Value);
        }

        // Test GetWarehouseById - Not Found
        [TestMethod]
        public async Task GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            _mockWarehouseService.Setup(service => service.GetWarehouseById(It.IsAny<int>())).ReturnsAsync((Warehouse)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test CreateWarehouse - Success
        [TestMethod]
        public async Task CreateWarehouse_ReturnsCreatedAtActionResult_WithCreatedWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                name = "Waardehuis"
            };
            _mockWarehouseService.Setup(service => service.AddWarehouse(warehouse)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.Create(warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(warehouse, createdResult.Value);
        }

        // Test UpdateWarehouse - Success
        [TestMethod]
        public async Task UpdateWarehouse_ReturnsOkResult_WithUpdatedWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                name = "Waardehuis"
            };
            _mockWarehouseService.Setup(service => service.UpdateWarehouse(warehouse)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(warehouse, okResult.Value);
        }

        // Test UpdateWarehouse - Not Found
        [TestMethod]
        public async Task UpdateWarehouse_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                name = "Waardehuis"
            };
            _mockWarehouseService.Setup(service => service.UpdateWarehouse(warehouse)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test DeleteWarehouse - Success
        [TestMethod]
        public async Task DeleteWarehouse_ReturnsNoContentResult_WhenWarehouseIsDeleted()
        {
            // Arrange
            _mockWarehouseService.Setup(service => service.DeleteWarehouse(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test DeleteWarehouse - Not Found
        [TestMethod]
        public async Task DeleteWarehouse_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            _mockWarehouseService.Setup(service => service.DeleteWarehouse(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}