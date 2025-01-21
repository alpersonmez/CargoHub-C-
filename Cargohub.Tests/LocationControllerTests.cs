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
    public class LocationControllerTests
    {
        private Mock<ILocationService> _mockLocationService;
        private LocationController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockLocationService = new Mock<ILocationService>();
            _controller = new LocationController(_mockLocationService.Object);
        }

        // Updated Test GetAllLocations
        [TestMethod]
        public async Task GetAllLocations_ReturnsOkResult_WithListOfLocations()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location
                {
                    id = 1,
                    warehouse_id = 101,
                    code = "LOC-001",
                    name = "36C",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow,
                    isdeleted = false
                },
                new Location
                {
                    id = 2,
                    warehouse_id = 102,
                    code = "LOC-002",
                    name = "Top Shelf",
                    created_at = DateTime.UtcNow.AddDays(-5),
                    updated_at = DateTime.UtcNow,
                    isdeleted = false
                }
            };

            // Adjusted Setup
            _mockLocationService.Setup(service => service.GetAllLocations(It.IsAny<int>())).ReturnsAsync(locations);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedLocations = okResult.Value as List<Location>;
            Assert.IsNotNull(returnedLocations);
            Assert.AreEqual(2, returnedLocations.Count);
            Assert.AreEqual("36C", returnedLocations[0].name);
            Assert.AreEqual("Top Shelf", returnedLocations[1].name);
        }


        // Test GetLocationById - Success
        [TestMethod]
        public async Task GetLocationById_ReturnsOkResult_WithLocation()
        {
            // Arrange
            var location = new Location
            {
                id = 1,
                warehouse_id = 101,
                code = "LOC-001",
                name = "36C",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockLocationService.Setup(service => service.GetLocationById(1)).ReturnsAsync(location);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(location, okResult.Value);
        }

        // Test GetLocationById - Not Found
        [TestMethod]
        public async Task GetLocationById_ReturnsNotFound_WhenLocationDoesNotExist()
        {
            // Arrange
            _mockLocationService.Setup(service => service.GetLocationById(It.IsAny<int>())).ReturnsAsync((Location)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test CreateLocation - Success
        [TestMethod]
        public async Task CreateLocation_ReturnsCreatedAtActionResult_WithCreatedLocation()
        {
            // Arrange
            var location = new Location
            {
                id = 1,
                warehouse_id = 101,
                code = "LOC-001",
                name = "36C",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockLocationService.Setup(service => service.AddLocation(location)).ReturnsAsync(location);

            // Act
            var result = await _controller.Create(location);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(location, createdResult.Value);
        }

        // Test UpdateLocation - Success
        [TestMethod]
        public async Task UpdateLocation_ReturnsOkResult_WithUpdatedLocation()
        {
            // Arrange
            var location = new Location
            {
                id = 1,
                warehouse_id = 101,
                code = "LOC-001",
                name = "Updated Location",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockLocationService.Setup(service => service.UpdateLocation(location)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, location);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(location, okResult.Value);
        }

        // Test UpdateLocation - Not Found
        [TestMethod]
        public async Task UpdateLocation_ReturnsNotFound_WhenLocationDoesNotExist()
        {
            // Arrange
            var location = new Location
            {
                id = 1,
                warehouse_id = 101,
                code = "LOC-001",
                name = "Nonexistent Location",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockLocationService.Setup(service => service.UpdateLocation(location)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, location);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test DeleteLocation - Success
        [TestMethod]
        public async Task DeleteLocation_ReturnsNoContentResult_WhenLocationIsDeleted()
        {
            // Arrange
            _mockLocationService.Setup(service => service.DeleteLocation(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test DeleteLocation - Not Found
        [TestMethod]
        public async Task DeleteLocation_ReturnsNotFound_WhenLocationDoesNotExist()
        {
            // Arrange
            _mockLocationService.Setup(service => service.DeleteLocation(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
