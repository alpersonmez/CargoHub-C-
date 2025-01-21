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

        [TestMethod]
        public async Task GetAllSuppliers_ReturnsOkResult_WithListOfSuppliers()
        {
            // Arrange
            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    id = 1,
                    code = "SUP001",
                    name = "John Doe",
                    address = "123 Main St",
                    address_extra = "Apt 4B",
                    city = "Springfield",
                    zip_code = "12345",
                    province = "State",
                    country = "USA",
                    contact_name = "Jane Smith",
                    phone_number = "555-1234",
                    reference = "REF001",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Supplier
                {
                    id = 2,
                    code = "SUP002",
                    name = "Harrie",
                    address = "456 Elm St",
                    address_extra = null,
                    city = "Shelbyville",
                    zip_code = "54321",
                    province = "Province",
                    country = "Canada",
                    contact_name = "John Smith",
                    phone_number = "555-5678",
                    reference = "REF002",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-15),
                    isdeleted = false
                }
            };
            _mockSupplierService.Setup(service => service.GetAllSuppliers(It.IsAny<int>())).ReturnsAsync(suppliers);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(suppliers, okResult.Value);
        }

        [TestMethod]
        public async Task GetSupplierById_ReturnsOkResult_WithSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                code = "SUP001",
                name = "John Doe",
                address = "123 Main St",
                address_extra = "Apt 4B",
                city = "Springfield",
                zip_code = "12345",
                province = "State",
                country = "USA",
                contact_name = "Jane Smith",
                phone_number = "555-1234",
                reference = "REF001",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task CreateSupplier_ReturnsCreatedAtActionResult_WithCreatedSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                code = "SUP001",
                name = "John Doe",
                address = "123 Main St",
                address_extra = "Apt 4B",
                city = "Springfield",
                zip_code = "12345",
                province = "State",
                country = "USA",
                contact_name = "Jane Smith",
                phone_number = "555-1234",
                reference = "REF001",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task UpdateSupplier_ReturnsOkResult_WithUpdatedSupplier()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                code = "SUP001",
                name = "John Doe",
                address = "123 Main St",
                address_extra = "Apt 4B",
                city = "Springfield",
                zip_code = "12345",
                province = "State",
                country = "USA",
                contact_name = "Jane Smith",
                phone_number = "555-1234",
                reference = "REF001",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
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

        [TestMethod]
        public async Task UpdateSupplier_ReturnsNotFound_WhenSupplierDoesNotExist()
        {
            // Arrange
            var supplier = new Supplier
            {
                id = 1,
                code = "SUP001",
                name = "John Doe",
                address = "123 Main St",
                address_extra = "Apt 4B",
                city = "Springfield",
                zip_code = "12345",
                province = "State",
                country = "USA",
                contact_name = "Jane Smith",
                phone_number = "555-1234",
                reference = "REF001",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
            };
            _mockSupplierService.Setup(service => service.UpdateSupplier(supplier)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

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
