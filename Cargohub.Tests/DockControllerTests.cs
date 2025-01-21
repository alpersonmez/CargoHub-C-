using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Controllers;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Cargohub.Tests
{
    [TestClass]
    public class DockControllerTests
    {
        private Mock<IDockService> _mockDockService;
        private DockController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockDockService = new Mock<IDockService>();
            _controller = new DockController(_mockDockService.Object);
        }

        [TestMethod]
        public async Task TestGetAllDocks()
        {
            // Arrange
            var docks = new List<Dock>
            {
                new Dock { id = 1, code = "DCK000001", status = "free", description = "Dock 1" },
                new Dock { id = 2, code = "DCK000002", status = "occupied", description = "Dock 2" }
            };
            _mockDockService.Setup(service => service.GetAllDocks(It.IsAny<int>())).ReturnsAsync(docks);

            // Act
            var result = await _controller.GetAll(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedDocks = okResult.Value as IEnumerable<object>;
            Assert.IsNotNull(returnedDocks);
            Assert.AreEqual(2, returnedDocks.Count());
        }

        [TestMethod]
        public async Task TestGetDockById()
        {
            // Arrange
            var dock = new Dock { id = 1, code = "DCK000001", status = "free", description = "Dock 1" };
            _mockDockService.Setup(service => service.GetDockById(1)).ReturnsAsync(dock);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedDock = okResult.Value as object;
            Assert.IsNotNull(returnedDock);
        }

        [TestMethod]
        public async Task TestGetDockByIdNotFound()
        {
            // Arrange
            _mockDockService.Setup(service => service.GetDockById(It.IsAny<int>())).ReturnsAsync((Dock)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task TestCreateDock()
        {
            // Arrange
            var dock = new Dock { warehouse_id = 1, description = "New Dock" };
            _mockDockService.Setup(service => service.AddDockAsync(It.IsAny<Dock>())).ReturnsAsync(dock);

            // Act
            var result = await _controller.Create(dock);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(dock, createdResult.Value);
        }

        [TestMethod]
        public async Task TestCreateDockInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("warehouse_id", "Warehouse ID is required");

            // Act
            var result = await _controller.Create(new Dock());

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TestUpdateDock()
        {
            // Arrange
            var dock = new Dock { id = 1, code = "DCK000001", status = "occupied", description = "Updated Dock" };
            _mockDockService.Setup(service => service.UpdateDockAsync(1, dock)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, dock);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestUpdateDockNotFound()
        {
            // Arrange
            var dock = new Dock { id = 1, code = "DCK000001", status = "occupied", description = "Updated Dock" };
            _mockDockService.Setup(service => service.UpdateDockAsync(1, dock)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, dock);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task TestDeleteDock()
        {
            // Arrange
            _mockDockService.Setup(service => service.RemoveDockAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDeleteDockNotFound()
        {
            // Arrange
            _mockDockService.Setup(service => service.RemoveDockAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
