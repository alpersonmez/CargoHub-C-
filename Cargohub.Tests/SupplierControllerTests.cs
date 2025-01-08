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
    public class SupplierControllerTests
    {
        private Mock<ISupplierService> _mockSupplierService;
        private SupplierController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockSupplierService = new Mock<ISupplierService>();
            _controller = new SupplierController(_mockSupplierService.Object);
        }

        // Test GetAllSuppliers
        [TestMethod]
        public async Task GetAllSuppliers_ReturnsOkResult_WithListOfSuppliers()
        {
            // Arrange
            var suppliers = new List<Supplier>
            {
                new Supplier{
                    id = 1,
                    name = "John Doe"
                },
                new Supplier{
                    id = 2,
                    name = "Harrie"
                }
            };
            _mockSupplierService.Setup(service => service.GetAllSuppliers(100)).ReturnsAsync(suppliers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(suppliers, okResult.Value);
        }

        // Test GetSupplierById - Success
        [TestMethod]
        public async Task GetSupplierById_ReturnsOkResult_WithSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                name = "John Doe"
            };
            _mockSupplierService.Setup(service => service.GetSupplierById(1)).ReturnsAsync(supplier);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(supplier, okResult.Value);
        }

        // Test GetSupplierById - Not Found
        [TestMethod]
        public async Task GetSupplierById_ReturnsNotFound_WhenSupplierDoesNotExist()
        {
            // Arrange
            _mockSupplierService.Setup(service => service.GetSupplierById(It.IsAny<int>())).ReturnsAsync((Supplier)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test CreateSupplier - Success
        [TestMethod]
        public async Task CreateSupplier_ReturnsCreatedAtActionResult_WithCreatedSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                name = "John Doe"
            };
            _mockSupplierService.Setup(service => service.AddSupplier(supplier)).ReturnsAsync(supplier);

            // Act
            var result = await _controller.Create(supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(supplier, createdResult.Value);
        }

        // Test UpdateSupplier - Success
        [TestMethod]
        public async Task UpdateSupplier_ReturnsOkResult_WithUpdatedSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                name = "John Doe"
            };
            _mockSupplierService.Setup(service => service.UpdateSupplier(supplier)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(supplier, okResult.Value);
        }

        // Test UpdateSupplier - Not Found
        [TestMethod]
        public async Task UpdateSupplier_ReturnsNotFound_WhenSupplierDoesNotExist()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                name = "John Doe"
            };
            _mockSupplierService.Setup(service => service.UpdateSupplier(supplier)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test DeleteSupplier - Success
        [TestMethod]
        public async Task DeleteSupplier_ReturnsNoContentResult_WhenSupplierIsDeleted()
        {
            // Arrange
            _mockSupplierService.Setup(service => service.DeleteSupplier(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test DeleteSupplier - Not Found
        [TestMethod]
        public async Task DeleteSupplier_ReturnsNotFound_WhenSupplierDoesNotExist()
        {
            // Arrange
            _mockSupplierService.Setup(service => service.DeleteSupplier(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}