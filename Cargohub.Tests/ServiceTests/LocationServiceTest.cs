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
    public class LocationServiceTests
    {
        private AppDbContext _context;
        private LocationService _locationService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _locationService = new LocationService(_context);

            // Seed the in-memory database
            _context.Locations.AddRange(new List<Location>
            {
                new Location
                {
                    id = 1,
                    warehouse_id = 100,
                    code = "LOC100",
                    name = "Location 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Location
                {
                    id = 2,
                    warehouse_id = 101,
                    code = "LOC101",
                    name = "Location 2",
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
        public async Task GetAllLocations_ReturnsCorrectAmount()
        {
            // Act
            var locations = await _locationService.GetAllLocations(2);

            // Assert
            Assert.AreEqual(2, locations.Count);
        }

        [TestMethod]
        public async Task GetLocationById_ReturnsCorrectLocation()
        {
            // Act
            var location = await _locationService.GetLocationById(1);

            // Assert
            Assert.IsNotNull(location);
            Assert.AreEqual("LOC100", location.code);
        }

        [TestMethod]
        public async Task GetLocationById_ReturnsNull_WhenLocationDoesNotExist()
        {
            // Act
            var location = await _locationService.GetLocationById(99);

            // Assert
            Assert.IsNull(location);
        }

        [TestMethod]
        public async Task AddLocation_AddsLocationSuccessfully()
        {
            // Arrange
            var newLocation = new Location
            {
                warehouse_id = 102,
                code = "LOC102",
                name = "Location 3",
            };

            // Act
            var addedLocation = await _locationService.AddLocation(newLocation);

            // Assert
            Assert.IsNotNull(addedLocation);
            Assert.AreEqual("LOC102", addedLocation.code);
            Assert.AreEqual(3, _context.Locations.Count());
        }

        [TestMethod]
        public async Task UpdateLocation_UpdatesExistingLocationSuccessfully()
        {
            // Arrange
            var existingLocation = await _locationService.GetLocationById(1);
            existingLocation.name = "Updated Location 1";

            // Act
            var updated = await _locationService.UpdateLocation(existingLocation);

            // Assert
            Assert.IsTrue(updated);
            var updatedLocation = await _locationService.GetLocationById(1);
            Assert.AreEqual("Updated Location 1", updatedLocation.name);
        }

        [TestMethod]
        public async Task UpdateLocation_ReturnsFalse_WhenLocationDoesNotExist()
        {
            // Arrange
            var nonExistingLocation = new Location
            {
                id = 99,
                warehouse_id = 103,
                code = "LOC103",
                name = "Nonexistent Location"
            };

            // Act
            var updated = await _locationService.UpdateLocation(nonExistingLocation);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteLocation_SoftDeletesLocationSuccessfully()
        {
            // Act
            var deleted = await _locationService.DeleteLocation(1);

            // Assert
            Assert.IsTrue(deleted);
            var location = await _locationService.GetLocationById(1);
            Assert.IsTrue(location.isdeleted);
        }

        [TestMethod]
        public async Task DeleteLocation_ReturnsFalse_WhenLocationDoesNotExist()
        {
            // Act
            var deleted = await _locationService.DeleteLocation(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
