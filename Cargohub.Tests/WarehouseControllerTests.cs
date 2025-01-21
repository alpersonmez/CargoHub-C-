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

        [TestMethod]
        public async Task GetAllWarehouses_ReturnsOkResult_WithListOfWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse>
            {
                new Warehouse
                {
                    id = 1,
                    code = "WH001",
                    name = "Main Warehouse",
                    address = "123 Main St",
                    zip = "12345",
                    city = "Springfield",
                    province = "State",
                    country = "USA",
                    contact = new Warehouse.Contact
                    {
                        name = "John Doe",
                        phone = "555-1234",
                        email = "johndoe@example.com"
                    },
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Warehouse
                {
                    id = 2,
                    code = "WH002",
                    name = "Secondary Warehouse",
                    address = "456 Elm St",
                    zip = "54321",
                    city = "Shelbyville",
                    province = "Province",
                    country = "Canada",
                    contact = new Warehouse.Contact
                    {
                        name = "Jane Smith",
                        phone = "555-5678",
                        email = "janesmith@example.com"
                    },
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-15),
                    isdeleted = false
                }
            };
            _mockWarehouseService.Setup(service => service.GetAllWarehouses(It.IsAny<int>())).ReturnsAsync(warehouses);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(warehouses, okResult.Value);
        }

        [TestMethod]
        public async Task GetWarehouseById_ReturnsOkResult_WithWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                code = "WH001",
                name = "Main Warehouse",
                address = "123 Main St",
                zip = "12345",
                city = "Springfield",
                province = "State",
                country = "USA",
                contact = new Warehouse.Contact
                {
                    name = "John Doe",
                    phone = "555-1234",
                    email = "johndoe@example.com"
                },
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task CreateWarehouse_ReturnsCreatedAtActionResult_WithCreatedWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                code = "WH001",
                name = "Main Warehouse",
                address = "123 Main St",
                zip = "12345",
                city = "Springfield",
                province = "State",
                country = "USA",
                contact = new Warehouse.Contact
                {
                    name = "John Doe",
                    phone = "555-1234",
                    email = "johndoe@example.com"
                },
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task UpdateWarehouse_ReturnsOkResult_WithUpdatedWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                code = "WH001",
                name = "Main Warehouse",
                address = "123 Main St",
                zip = "12345",
                city = "Springfield",
                province = "State",
                country = "USA",
                contact = new Warehouse.Contact
                {
                    name = "John Doe",
                    phone = "555-1234",
                    email = "johndoe@example.com"
                },
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task UpdateWarehouse_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var warehouse = new Warehouse
            {
                id = 1,
                code = "WH001",
                name = "Main Warehouse",
                address = "123 Main St",
                zip = "12345",
                city = "Springfield",
                province = "State",
                country = "USA",
                contact = new Warehouse.Contact
                {
                    name = "John Doe",
                    phone = "555-1234",
                    email = "johndoe@example.com"
                },
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockWarehouseService.Setup(service => service.UpdateWarehouse(warehouse)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

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
