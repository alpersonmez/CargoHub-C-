using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Controllers;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class ShipmentControllerTests
    {
        private Mock<IShipmentService> _mockShipmentService;
        private Mock<IOrderShipmentService> _mockOrderShipmentService;
        private ShipmentController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockShipmentService = new Mock<IShipmentService>();
            _mockOrderShipmentService = new Mock<IOrderShipmentService>();
            _controller = new ShipmentController(_mockShipmentService.Object, _mockOrderShipmentService.Object);
        }

        [TestMethod]
        public async Task GetAllShipments_ReturnsOkResult_WithListOfShipments()
        {
            // Arrange
            var shipments = new List<Shipment>
            {
                new Shipment
                {
                    id = 1,
                    source_id = 1001,
                    order_date = DateTime.UtcNow.AddDays(-10),
                    request_date = DateTime.UtcNow.AddDays(-7),
                    shipment_date = DateTime.UtcNow.AddDays(-5),
                    shipment_type = "Express",
                    shipment_status = "Pending",
                    notes = "First shipment notes",
                    carrier_code = "CC001",
                    carrier_description = "Carrier Description 1",
                    service_code = "SC001",
                    payment_type = "Prepaid",
                    transfer_mode = "Air",
                    total_package_count = 10,
                    total_package_weight = 150.5,
                    created_at = DateTime.UtcNow.AddDays(-15),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                },
                new Shipment
                {
                    id = 2,
                    source_id = 1002,
                    order_date = DateTime.UtcNow.AddDays(-20),
                    request_date = DateTime.UtcNow.AddDays(-15),
                    shipment_date = DateTime.UtcNow.AddDays(-10),
                    shipment_type = "Standard",
                    shipment_status = "Completed",
                    notes = "Second shipment notes",
                    carrier_code = "CC002",
                    carrier_description = "Carrier Description 2",
                    service_code = "SC002",
                    payment_type = "Postpaid",
                    transfer_mode = "Sea",
                    total_package_count = 20,
                    total_package_weight = 500.75,
                    created_at = DateTime.UtcNow.AddDays(-25),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                }
            };
            _mockShipmentService.Setup(service => service.GetAllShipments(100)).ReturnsAsync(shipments);

            // Act
            var result = await _controller.GetAll(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedShipments = okResult.Value as List<Shipment>;
            Assert.IsNotNull(returnedShipments);
            Assert.AreEqual(2, returnedShipments.Count);
        }

        [TestMethod]
        public async Task GetShipmentById_ReturnsOkResult_WithShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                source_id = 1001,
                order_date = DateTime.UtcNow.AddDays(-10),
                request_date = DateTime.UtcNow.AddDays(-7),
                shipment_date = DateTime.UtcNow.AddDays(-5),
                shipment_type = "Express",
                shipment_status = "Pending",
                notes = "First shipment notes",
                carrier_code = "CC001",
                carrier_description = "Carrier Description 1",
                service_code = "SC001",
                payment_type = "Prepaid",
                transfer_mode = "Air",
                total_package_count = 10,
                total_package_weight = 150.5,
                created_at = DateTime.UtcNow.AddDays(-15),
                updated_at = DateTime.UtcNow.AddDays(-10),
                isdeleted = false
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

        [TestMethod]
        public async Task CreateShipment_ReturnsCreatedAtActionResult_WithCreatedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                source_id = 1001,
                order_date = DateTime.UtcNow.AddDays(-10),
                request_date = DateTime.UtcNow.AddDays(-7),
                shipment_date = DateTime.UtcNow.AddDays(-5),
                shipment_type = "Express",
                shipment_status = "Pending",
                notes = "First shipment notes",
                carrier_code = "CC001",
                carrier_description = "Carrier Description 1",
                service_code = "SC001",
                payment_type = "Prepaid",
                transfer_mode = "Air",
                total_package_count = 10,
                total_package_weight = 150.5
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

        [TestMethod]
        public async Task UpdateShipment_ReturnsOkResult_WithUpdatedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Updated shipment notes",
                updated_at = DateTime.UtcNow
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

        [TestMethod]
        public async Task UpdateShipment_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            var shipment = new Shipment
            {
                id = 1,
                notes = "Updated shipment notes",
                updated_at = DateTime.UtcNow
            };
            _mockShipmentService.Setup(service => service.UpdateShipment(shipment)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

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
