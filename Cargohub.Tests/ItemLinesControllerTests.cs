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
    public class ItemLinesControllerTests
    {
        private Mock<IItemLinesService> _mockItemLinesService;
        private ItemLinesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockItemLinesService = new Mock<IItemLinesService>();
            _controller = new ItemLinesController(_mockItemLinesService.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfItemLines()
        {
            // Arrange
            var itemLines = new List<ItemLines>
            {
                new ItemLines
                {
                    id = 1,
                    name = "Line A",
                    description = "Description A",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemLines
                {
                    id = 2,
                    name = "Line B",
                    description = "Description B",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            };

            _mockItemLinesService.Setup(service => service.GetAllItemLines(It.IsAny<int>())).ReturnsAsync(itemLines);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedItemLines = okResult.Value as List<ItemLines>;
            Assert.IsNotNull(returnedItemLines);
            Assert.AreEqual(2, returnedItemLines.Count);
        }

        [TestMethod]
        public async Task GetById_ReturnsOkResult_WithItemLine()
        {
            // Arrange
            var itemLine = new ItemLines
            {
                id = 1,
                name = "Line A",
                description = "Description A",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };

            _mockItemLinesService.Setup(service => service.GetItemLineById(1)).ReturnsAsync(itemLine);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(itemLine, okResult.Value);
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenItemLineDoesNotExist()
        {
            // Arrange
            _mockItemLinesService.Setup(service => service.GetItemLineById(It.IsAny<int>())).ReturnsAsync((ItemLines)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Update_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var itemLine = new ItemLines
            {
                id = 1,
                name = "Updated Line",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };

            _mockItemLinesService.Setup(service => service.UpdateItemLine(itemLine)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, itemLine);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(true, okResult.Value);
        }

        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenItemLineDoesNotExist()
        {
            // Arrange
            var itemLine = new ItemLines
            {
                id = 1,
                name = "Updated Line",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };

            _mockItemLinesService.Setup(service => service.UpdateItemLine(itemLine)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, itemLine);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContentResult_WhenDeletionIsSuccessful()
        {
            // Arrange
            _mockItemLinesService.Setup(service => service.DeleteItemLine(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenItemLineDoesNotExist()
        {
            // Arrange
            _mockItemLinesService.Setup(service => service.DeleteItemLine(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
