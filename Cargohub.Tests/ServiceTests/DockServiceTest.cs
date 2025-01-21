using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Cargohub.Tests
{
    [TestClass]
    public class DockServiceTests
    {
        private AppDbContext _context;
        private DockService _dockService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _dockService = new DockService(_context);

            // Seed the in-memory database
            _context.Docks.AddRange(new List<Dock>
            {
                new Dock
                {
                    id = 1,
                    warehouse_id = 101,
                    code = "DCK000001",
                    status = "free",
                    description = "Dock 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    is_deleted = false
                },
                new Dock
                {
                    id = 2,
                    warehouse_id = 102,
                    code = "DCK000002",
                    status = "occupied",
                    description = "Dock 2",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    is_deleted = false
                }
            });
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllDocks_ReturnsCorrectAmount()
        {
            // Act
            var docks = await _dockService.GetAllDocks(2);

            // Assert
            Assert.AreEqual(2, docks.Count);
        }

        [TestMethod]
        public async Task GetDockById_ReturnsCorrectDock()
        {
            // Act
            var dock = await _dockService.GetDockById(1);

            // Assert
            Assert.IsNotNull(dock);
            Assert.AreEqual("DCK000001", dock.code);
            Assert.AreEqual("free", dock.status);
        }

        [TestMethod]
        public async Task GetDockById_ReturnsNull_WhenDockDoesNotExist()
        {
            // Act
            var dock = await _dockService.GetDockById(99);

            // Assert
            Assert.IsNull(dock);
        }

        [TestMethod]
        public async Task AddDock_AddsDockSuccessfully()
        {
            // Arrange
            var newDock = new Dock
            {
                warehouse_id = 103,
                description = "New Dock"
            };

            // Act
            var addedDock = await _dockService.AddDockAsync(newDock);

            // Assert
            Assert.IsNotNull(addedDock);
            Assert.AreEqual("DCK000003", addedDock.code);
            Assert.AreEqual("New Dock", addedDock.description);
        }

        [TestMethod]
        public async Task UpdateDock_UpdatesExistingDockSuccessfully()
        {
            // Arrange
            var existingDock = await _dockService.GetDockById(1);
            existingDock.status = "occupied";

            // Act
            var updated = await _dockService.UpdateDockAsync(1, existingDock);

            // Assert
            Assert.IsTrue(updated);
            var updatedDock = await _dockService.GetDockById(1);
            Assert.AreEqual("occupied", updatedDock.status);
        }

        [TestMethod]
        public async Task UpdateDock_ReturnsFalse_WhenDockDoesNotExist()
        {
            // Arrange
            var nonExistingDock = new Dock
            {
                id = 99,
                warehouse_id = 105,
                description = "Nonexistent Dock"
            };

            // Act
            var updated = await _dockService.UpdateDockAsync(99, nonExistingDock);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteDock_SoftDeletesDockSuccessfully()
        {
            // Act
            var deleted = await _dockService.RemoveDockAsync(1);

            // Assert
            Assert.IsTrue(deleted);
            var dock = await _dockService.GetDockById(1);
            Assert.IsTrue(dock.is_deleted);
        }

        [TestMethod]
        public async Task DeleteDock_ReturnsFalse_WhenDockDoesNotExist()
        {
            // Act
            var deleted = await _dockService.RemoveDockAsync(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
