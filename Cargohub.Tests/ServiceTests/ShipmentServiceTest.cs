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
    public class ShipmentServiceTests
    {
        private AppDbContext _context;
        private ShipmentService _shipmentService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _shipmentService = new ShipmentService(_context);

            // Seed the in-memory database
            _context.Shipments.AddRange(new List<Shipment>
            {
                new Shipment
                {
                    id = 1,
                    source_id = 100,
                    order_date = DateTime.UtcNow.AddDays(-10),
                    request_date = DateTime.UtcNow.AddDays(-8),
                    shipment_date = DateTime.UtcNow.AddDays(-5),
                    shipment_type = "Standard",
                    shipment_status = "Shipped",
                    notes = "Test shipment 1",
                    carrier_code = "CARR001",
                    total_package_count = 3,
                    total_package_weight = 50.0,
                    created_at = DateTime.UtcNow.AddDays(-15),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Shipment
                {
                    id = 2,
                    source_id = 101,
                    order_date = DateTime.UtcNow.AddDays(-12),
                    request_date = DateTime.UtcNow.AddDays(-10),
                    shipment_date = DateTime.UtcNow.AddDays(-7),
                    shipment_type = "Express",
                    shipment_status = "Delivered",
                    notes = "Test shipment 2",
                    carrier_code = "CARR002",
                    total_package_count = 5,
                    total_package_weight = 120.0,
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-7),
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
        public async Task GetAllShipments_ReturnsCorrectAmount()
        {
            // Act
            var shipments = await _shipmentService.GetAllShipments(2);

            // Assert
            Assert.AreEqual(2, shipments.Count);
        }

        [TestMethod]
        public async Task GetShipmentById_ReturnsCorrectShipment()
        {
            // Act
            var shipment = await _shipmentService.GetShipmentById(1);

            // Assert
            Assert.IsNotNull(shipment);
            Assert.AreEqual(100, shipment.source_id);
        }

        [TestMethod]
        public async Task GetShipmentById_ReturnsNull_WhenShipmentDoesNotExist()
        {
            // Act
            var shipment = await _shipmentService.GetShipmentById(99);

            // Assert
            Assert.IsNull(shipment);
        }

        [TestMethod]
        public async Task AddShipment_AddsShipmentSuccessfully()
        {
            // Arrange
            var newShipment = new Shipment
            {
                source_id = 102,
                order_date = DateTime.UtcNow.AddDays(-3),
                request_date = DateTime.UtcNow.AddDays(-2),
                shipment_date = DateTime.UtcNow,
                shipment_type = "Overnight",
                shipment_status = "Pending",
                notes = "Test shipment 3",
                carrier_code = "CARR003",
                total_package_count = 2,
                total_package_weight = 30.0
            };

            // Act
            var addedShipment = await _shipmentService.AddShipment(newShipment);

            // Assert
            Assert.IsNotNull(addedShipment);
            Assert.AreEqual("Overnight", addedShipment.shipment_type);
            Assert.AreEqual(3, _context.Shipments.Count());
        }

        [TestMethod]
        public async Task UpdateShipment_UpdatesExistingShipmentSuccessfully()
        {
            // Arrange
            var existingShipment = await _shipmentService.GetShipmentById(1);
            existingShipment.notes = "Updated shipment notes";

            // Act
            var updated = await _shipmentService.UpdateShipment(existingShipment);

            // Assert
            Assert.IsTrue(updated);
            var updatedShipment = await _shipmentService.GetShipmentById(1);
            Assert.AreEqual("Updated shipment notes", updatedShipment.notes);
        }

        [TestMethod]
        public async Task UpdateShipment_ReturnsFalse_WhenShipmentDoesNotExist()
        {
            // Arrange
            var nonExistingShipment = new Shipment
            {
                id = 99,
                notes = "Nonexistent shipment notes"
            };

            // Act
            var updated = await _shipmentService.UpdateShipment(nonExistingShipment);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteShipment_SoftDeletesShipmentSuccessfully()
        {
            // Act
            var deleted = await _shipmentService.DeleteShipment(1);

            // Assert
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task DeleteShipment_ReturnsFalse_WhenShipmentDoesNotExist()
        {
            // Act
            var deleted = await _shipmentService.DeleteShipment(99);

            // Assert
            Assert.IsFalse(deleted);
        }

        [TestMethod]
        public async Task DisconnectOrdersFromShipment_RemovesLinksSuccessfully()
        {
            // Arrange
            var shipment = await _shipmentService.GetShipmentById(1);
            shipment.OrderShipments = new List<OrderShipment>
            {
                new OrderShipment { shipment_id = 1, order_id = 1 },
                new OrderShipment { shipment_id = 1, order_id = 2 }
            };

            _context.OrderShipments.AddRange(shipment.OrderShipments);
            await _context.SaveChangesAsync();

            // Act
            var result = await _shipmentService.DisconnectOrdersFromShipment(1, new List<int> { 1 });

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, shipment.OrderShipments.Count);
        }
    }
}
