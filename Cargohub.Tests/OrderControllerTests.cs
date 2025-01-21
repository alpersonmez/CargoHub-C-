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
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private Mock<IOrderShipmentService> _mockOrderShipmentService;
        private OrderController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockOrderShipmentService = new Mock<IOrderShipmentService>();
            _controller = new OrderController(_mockOrderService.Object, _mockOrderShipmentService.Object);
        }

        [TestMethod]
        public async Task GetAllOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<OrderDto>
            {
                new OrderDto
                {
                    id = 1,
                    source_id = 33,
                    order_date = DateTime.UtcNow.AddDays(-5),
                    request_date = DateTime.UtcNow.AddDays(1),
                    reference = "ORD00001",
                    reference_extra = "Extra reference",
                    order_status = "Delivered",
                    notes = "Test notes",
                    shipping_notes = "Shipping test notes",
                    picking_notes = "Picking test notes",
                    warehouse_id = 18,
                    ship_to = "Germany",
                    bill_to = "Netherlands",
                    total_amount = 1500.50,
                    total_discount = 50.00,
                    total_tax = 300.00,
                    total_surcharge = 20.00,
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow
                },
                new OrderDto
                {
                    id = 2,
                    source_id = 44,
                    order_date = DateTime.UtcNow.AddDays(-3),
                    request_date = DateTime.UtcNow.AddDays(2),
                    reference = "ORD00002",
                    reference_extra = "Another reference",
                    order_status = "Pending",
                    notes = "Another test note",
                    shipping_notes = "Another shipping note",
                    picking_notes = "Another picking note",
                    warehouse_id = 19,
                    ship_to = "France",
                    bill_to = "Italy",
                    total_amount = 2000.75,
                    total_discount = 75.00,
                    total_tax = 400.00,
                    total_surcharge = 25.00,
                    created_at = DateTime.UtcNow.AddDays(-8),
                    updated_at = DateTime.UtcNow
                }
            };

            _mockOrderService.Setup(service => service.GetAllOrders(It.IsAny<int>())).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedOrders = okResult.Value as List<OrderDto>;
            Assert.IsNotNull(returnedOrders);
            Assert.AreEqual(2, returnedOrders.Count);
        }

        [TestMethod]
        public async Task GetOrderById_ReturnsOkResult_WithOrder()
        {
            // Arrange
            var order = new OrderDto
            {
                id = 1,
                source_id = 33,
                order_date = DateTime.UtcNow.AddDays(-5),
                request_date = DateTime.UtcNow.AddDays(1),
                reference = "ORD00001",
                reference_extra = "Extra reference",
                order_status = "Delivered",
                notes = "Test notes",
                shipping_notes = "Shipping test notes",
                picking_notes = "Picking test notes",
                warehouse_id = 18,
                ship_to = "Germany",
                bill_to = "Netherlands",
                total_amount = 1500.50,
                total_discount = 50.00,
                total_tax = 300.00,
                total_surcharge = 20.00,
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow
            };

            _mockOrderService.Setup(service => service.GetOrderById(1)).ReturnsAsync(order);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(order, okResult.Value);
        }

        [TestMethod]
        public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderService.Setup(service => service.GetOrderById(It.IsAny<int>())).ReturnsAsync((OrderDto)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateOrder_ReturnsCreatedAtActionResult_WithCreatedOrder()
        {
            // Arrange
            var newOrder = new OrderDto
            {
                source_id = 33,
                order_date = DateTime.UtcNow.AddDays(-5),
                request_date = DateTime.UtcNow.AddDays(1),
                reference = "ORD00001",
                order_status = "Delivered"
            };

            var createdOrder = new OrderDto
            {
                id = 1,
                source_id = 33,
                order_date = DateTime.UtcNow.AddDays(-5),
                request_date = DateTime.UtcNow.AddDays(1),
                reference = "ORD00001",
                order_status = "Delivered"
            };

            _mockOrderService.Setup(service => service.AddOrder(newOrder)).ReturnsAsync(createdOrder);

            // Act
            var result = await _controller.Create(newOrder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(createdOrder, createdResult.Value);
        }

        [TestMethod]
        public async Task UpdateOrder_ReturnsOkResult_WithUpdatedOrder()
        {
            // Arrange
            var updatedOrder = new OrderDto
            {
                id = 1,
                source_id = 33,
                reference = "ORD00001",
                order_status = "Shipped"
            };

            _mockOrderService.Setup(service => service.UpdateOrder(updatedOrder)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, updatedOrder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedOrder, okResult.Value);
        }

        [TestMethod]
        public async Task UpdateOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var updatedOrder = new OrderDto
            {
                id = 1,
                source_id = 33,
                reference = "ORD00001",
                order_status = "Shipped"
            };

            _mockOrderService.Setup(service => service.UpdateOrder(updatedOrder)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, updatedOrder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteOrder_ReturnsNoContentResult_WhenOrderIsDeleted()
        {
            // Arrange
            _mockOrderService.Setup(service => service.DeleteOrder(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderService.Setup(service => service.DeleteOrder(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
