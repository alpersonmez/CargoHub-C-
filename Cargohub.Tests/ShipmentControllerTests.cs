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
    public class ShipmentControllerTests
    {
        private Mock<IShipmentService> _mockShipmentService;
        private ShipmentController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockShipmentService = new Mock<IShipmentService>();
            _controller = new ShipmentController(_mockShipmentService.Object);
        }

        // Test GetAllShipments
        [TestMethod]
        public async Task GetAllShipments_ReturnsOkResult_WithListOfShipments()
        {
            // Arrange
            var shipments = new List<Shipment>
            {
                new Shipment{
                    id = 1,
                    notes = "Eerste"
                },
                new Shipment{
                    id = 2,
                    notes = "Stoomboot"
                }
            };
            _mockShipmentService.Setup(service => service.GetAllShipments(100)).ReturnsAsync(shipments);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(shipments, okResult.Value);
        }

        // Test GetShipmentById - Success
        [TestMethod]
        public async Task GetShipmentById_ReturnsOkResult_WithShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Eerste"
            };
            _mockShipmentService.Setup(service => service.GetShipmentById(1)).ReturnsAsync(shipment);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(shipment, okResult.Value);
        }

        // Test GetShipmentById - Not Found
        [TestMethod]
        public async Task GetShipmentById_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            _mockShipmentService.Setup(service => service.GetShipmentById(It.IsAny<int>())).ReturnsAsync((Shipment)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test CreateShipment - Success
        [TestMethod]
        public async Task CreateShipment_ReturnsCreatedAtActionResult_WithCreatedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Eerste"
            };
            _mockShipmentService.Setup(service => service.AddShipment(shipment)).ReturnsAsync(shipment);

            // Act
            var result = await _controller.Create(shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(shipment, createdResult.Value);
        }

        // Test UpdateShipment - Success
        [TestMethod]
        public async Task UpdateShipment_ReturnsOkResult_WithUpdatedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Eerste"
            };
            _mockShipmentService.Setup(service => service.UpdateShipment(shipment)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(shipment, okResult.Value);
        }

        // Test UpdateShipment - Not Found
        [TestMethod]
        public async Task UpdateShipment_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Eerste"
            };
            _mockShipmentService.Setup(service => service.UpdateShipment(shipment)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test DeleteShipment - Success
        [TestMethod]
        public async Task DeleteShipment_ReturnsNoContentResult_WhenShipmentIsDeleted()
        {
            // Arrange
            _mockShipmentService.Setup(service => service.DeleteShipment(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test DeleteShipment - Not Found
        [TestMethod]
        public async Task DeleteShipment_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            _mockShipmentService.Setup(service => service.DeleteShipment(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}