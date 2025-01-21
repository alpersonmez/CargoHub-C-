using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private AppDbContext _context;
        private OrderService _orderService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _orderService = new OrderService(_context);

            // Seed the in-memory database
            _context.Orders.AddRange(new List<Order>
            {
                new Order
                {
                    id = 1,
                    source_id = 100,
                    order_date = DateTime.UtcNow.AddDays(-10),
                    request_date = DateTime.UtcNow.AddDays(-5),
                    reference = "REF100",
                    order_status = "Pending",
                    total_amount = 1000,
                    total_discount = 50,
                    total_tax = 150,
                    total_surcharge = 20,
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Order
                {
                    id = 2,
                    source_id = 101,
                    order_date = DateTime.UtcNow.AddDays(-20),
                    request_date = DateTime.UtcNow.AddDays(-15),
                    reference = "REF101",
                    order_status = "Completed",
                    total_amount = 2000,
                    total_discount = 100,
                    total_tax = 300,
                    total_surcharge = 50,
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
        public async Task GetAllOrders_ReturnsCorrectAmount()
        {
            // Act
            var orders = await _orderService.GetAllOrders(2);

            // Assert
            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod]
        public async Task GetOrderById_ReturnsCorrectOrderDto()
        {
            // Act
            var orderDto = await _orderService.GetOrderById(1);

            // Assert
            Assert.IsNotNull(orderDto);
            Assert.AreEqual("REF100", orderDto.reference);
        }

        [TestMethod]
        public async Task GetOrderById_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Act
            var orderDto = await _orderService.GetOrderById(99);

            // Assert
            Assert.IsNull(orderDto);
        }

        [TestMethod]
        public async Task AddOrder_AddsOrderSuccessfully()
        {
            // Arrange
            var newOrderDto = new OrderDto
            {
                source_id = 102,
                order_date = DateTime.UtcNow,
                request_date = DateTime.UtcNow.AddDays(5),
                reference = "REF102",
                order_status = "Pending",
                total_amount = 1500,
                total_discount = 75,
                total_tax = 225,
                total_surcharge = 30
            };

            // Act
            var addedOrderDto = await _orderService.AddOrder(newOrderDto);

            // Assert
            Assert.IsNotNull(addedOrderDto);
            Assert.AreEqual("REF102", addedOrderDto.reference);
            Assert.AreEqual(3, _context.Orders.Count());
        }

        [TestMethod]
        public async Task UpdateOrder_UpdatesExistingOrderSuccessfully()
        {
            // Arrange
            var existingOrderDto = await _orderService.GetOrderById(1);
            existingOrderDto.order_status = "Shipped";

            // Act
            var updated = await _orderService.UpdateOrder(existingOrderDto);

            // Assert
            Assert.IsTrue(updated);
            var updatedOrderDto = await _orderService.GetOrderById(1);
            Assert.AreEqual("Shipped", updatedOrderDto.order_status);
        }

        [TestMethod]
        public async Task UpdateOrder_ReturnsFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var nonExistingOrderDto = new OrderDto
            {
                id = 99,
                order_status = "Shipped"
            };

            // Act
            var updated = await _orderService.UpdateOrder(nonExistingOrderDto);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteOrder_SoftDeletesOrderSuccessfully()
        {
            // Act
            var deleted = await _orderService.DeleteOrder(1);

            // Assert
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task DeleteOrder_ReturnsFalse_WhenOrderDoesNotExist()
        {
            // Act
            var deleted = await _orderService.DeleteOrder(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
