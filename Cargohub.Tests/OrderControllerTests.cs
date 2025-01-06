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
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private OrderController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }

        // Test GetAllOrders
        [TestMethod]
        public void GetAllOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order {
                    id = 1,
                    source_id = 33,
                    order_date = DateTime.Parse("2019-04-03T11:33:15Z"),
                    request_date = DateTime.Parse("2019-04-07T11:33:15Z"),
                    reference = "ORD00001",
                    reference_extra = "Bedreven arm straffen bureau.",
                    order_status = "Delivered",
                    notes = "Voedsel vijf vork heel.",
                    shipping_notes = "Buurman betalen plaats bewolkt.",
                    picking_notes = "Ademen fijn volgorde scherp aardappel op leren.",
                    warehouse_id = 18,
                    ship_to = "Duitsland",
                    bill_to = "Nederland",
                    shipment_id = 1
                },
                new Order
                {
                    id = 2,
                    source_id = 44,
                    order_date = DateTime.Parse("2019-04-03T11:33:15Z"),
                    request_date = DateTime.Parse("2019-04-07T11:33:15Z"),
                    reference = "ORD00001",
                    reference_extra = "Bedreven arm straffen bureau.",
                    order_status = "Delivered",
                    notes = "Voedsel vijf vork heel.",
                    shipping_notes = "Buurman betalen plaats bewolkt.",
                    picking_notes = "Ademen fijn volgorde scherp aardappel op leren.",
                    warehouse_id = 18,
                    ship_to = "Duitsland",
                    bill_to = "Nederland",
                    shipment_id = 1,
                    total_amount = 9905.13,
                    total_discount = 150.77,
                    total_tax = 372.72,
                    total_surcharge = 77.6,
                    created_at = DateTime.Parse("2019-04-03T11:33:15Z"),
                    updated_at = DateTime.Parse("2019-04-05T07:33:15Z")
                }
                };
            _mockOrderService.Setup(service => service.GetAllOrders(100)).Returns(orders);

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(orders, okResult.Value);
        }

        // // Test GetOrderById - Success
        // [TestMethod]
        // public void GetOrderById_ReturnsOkResult_WithOrder()
        // {
        //     // Arrange
        //     var order = new Order { id = 1, name = "Order A" };
        //     _mockOrderService.Setup(service => service.GetOrder(1)).Returns(order);

        //     // Act
        //     var result = _controller.GetOrder(1);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //     var okResult = result as OkObjectResult;
        //     Assert.IsNotNull(okResult);
        //     Assert.AreEqual(order, okResult.Value);
        // }

        // // Test GetOrderById - Not Found
        // [TestMethod]
        // public void GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
        // {
        //     // Arrange
        //     _mockOrderService.Setup(service => service.GetOrderById(It.IsAny<int>())).Returns((Order)null);

        //     // Act
        //     var result = _controller.GetOrderById(1);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        // }

        // // Test CreateOrder - Success
        // [TestMethod]
        // public void CreateOrder_ReturnsCreatedAtActionResult_WithCreatedOrder()
        // {
        //     // Arrange
        //     var order = new Order { id = 1, name = "Order A" };
        //     _mockOrderService.Setup(service => service.CreateOrder(order)).Returns(order);

        //     // Act
        //     var result = _controller.CreateOrder(order);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        //     var createdResult = result as CreatedAtActionResult;
        //     Assert.IsNotNull(createdResult);
        //     Assert.AreEqual(order, createdResult.Value);
        // }

        // // Test UpdateOrder - Success
        // [TestMethod]
        // public void UpdateOrder_ReturnsOkResult_WithUpdatedOrder()
        // {
        //     // Arrange
        //     var order = new Order { Id = 1, name = "Updated Order" };
        //     _mockOrderService.Setup(service => service.UpdateOrder(order)).Returns(order);

        //     // Act
        //     var result = _controller.UpdateOrder(1, order);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //     var okResult = result as OkObjectResult;
        //     Assert.IsNotNull(okResult);
        //     Assert.AreEqual(order, okResult.Value);
        // }

        // // Test UpdateOrder - Not Found
        // [TestMethod]
        // public void UpdateOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        // {
        //     // Arrange
        //     var order = new Order { id = 1, name = "Updated Order" };
        //     _mockOrderService.Setup(service => service.UpdateOrder(order)).Returns((Order)null);

        //     // Act
        //     var result = _controller.UpdateOrder(1, order);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        // }

        // // Test DeleteOrder - Success
        // [TestMethod]
        // public void DeleteOrder_ReturnsNoContentResult_WhenOrderIsDeleted()
        // {
        //     // Arrange
        //     _mockOrderService.Setup(service => service.DeleteOrder(1)).Returns(true);

        //     // Act
        //     var result = _controller.DeleteOrder(1);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(NoContentResult));
        // }

        // // Test DeleteOrder - Not Found
        // [TestMethod]
        // public void DeleteOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        // {
        //     // Arrange
        //     _mockOrderService.Setup(service => service.DeleteOrder(1)).Returns(false);

        //     // Act
        //     var result = _controller.DeleteOrder(1);

        //     // Assert
        //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        // }
    }
}