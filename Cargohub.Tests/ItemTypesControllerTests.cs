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
    public class ItemTypesControllerTests
    {
        private Mock<IItemTypeService> _mockItemTypeService;
        private ItemTypesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockItemTypeService = new Mock<IItemTypeService>();
            _controller = new ItemTypesController(_mockItemTypeService.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfItemTypes()
        {
            // Arrange
            var itemTypes = new List<ItemType>
            {
                new ItemType
                {
                    id = 1,
                    name = "Type1",
                    description = "Description of Type1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemType
                {
                    id = 2,
                    name = "Type2",
                    description = "Description of Type2",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            };
            _mockItemTypeService.Setup(service => service.GetAllItemTypes(It.IsAny<int>())).ReturnsAsync(itemTypes);

            // Act
            var result = await _controller.GetAll(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedItemTypes = okResult.Value as List<ItemType>;
            Assert.IsNotNull(returnedItemTypes);
            Assert.AreEqual(2, returnedItemTypes.Count);
            Assert.AreEqual("Type1", returnedItemTypes[0].name);
            Assert.AreEqual("Type2", returnedItemTypes[1].name);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithItemType()
        {
            // Arrange
            var itemType = new ItemType
            {
                id = 1,
                name = "Type1",
                description = "Description of Type1",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockItemTypeService.Setup(service => service.GetItemTypeById(1)).ReturnsAsync(itemType);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(itemType, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsNotFound_WhenItemTypeDoesNotExist()
        {
            // Arrange
            _mockItemTypeService.Setup(service => service.GetItemTypeById(It.IsAny<int>())).ReturnsAsync((ItemType)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Update_ReturnsOkResult_WithUpdatedItemType()
        {
            // Arrange
            var updatedItemType = new ItemType
            {
                id = 1,
                name = "Updated Type",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemTypeService.Setup(service => service.UpdateItemType(updatedItemType)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, updatedItemType);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedItemType, okResult.Value);
        }

        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenItemTypeDoesNotExist()
        {
            // Arrange
            var updatedItemType = new ItemType
            {
                id = 1,
                name = "Updated Type",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemTypeService.Setup(service => service.UpdateItemType(updatedItemType)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, updatedItemType);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContentResult_WhenItemTypeIsDeleted()
        {
            // Arrange
            _mockItemTypeService.Setup(service => service.DeleteItemType(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenItemTypeDoesNotExist()
        {
            // Arrange
            _mockItemTypeService.Setup(service => service.DeleteItemType(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
