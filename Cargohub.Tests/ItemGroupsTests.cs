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
    public class ItemGroupsControllerTests
    {
        private Mock<IItemGroupService> _mockItemGroupService;
        private Item_GroupsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockItemGroupService = new Mock<IItemGroupService>();
            _controller = new Item_GroupsController(_mockItemGroupService.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfItemGroups()
        {
            // Arrange
            var itemGroups = new List<ItemGroup>
            {
                new ItemGroup
                {
                    id = 1,
                    name = "Group 1",
                    description = "Description for Group 1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new ItemGroup
                {
                    id = 2,
                    name = "Group 2",
                    description = "Description for Group 2",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            };
            _mockItemGroupService.Setup(service => service.GetAllItemGroups(It.IsAny<int>())).ReturnsAsync(itemGroups);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(itemGroups, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithItemGroup()
        {
            // Arrange
            var itemGroup = new ItemGroup
            {
                id = 1,
                name = "Group 1",
                description = "Description for Group 1",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockItemGroupService.Setup(service => service.GetItemGroupById(1)).ReturnsAsync(itemGroup);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(itemGroup, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsNotFound_WhenItemGroupDoesNotExist()
        {
            // Arrange
            _mockItemGroupService.Setup(service => service.GetItemGroupById(It.IsAny<int>())).ReturnsAsync((ItemGroup)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Update_ReturnsNoContentResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var itemGroup = new ItemGroup
            {
                id = 1,
                name = "Updated Group",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemGroupService.Setup(service => service.UpdateItem_Groups(itemGroup)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, itemGroup);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenItemGroupDoesNotExist()
        {
            // Arrange
            var itemGroup = new ItemGroup
            {
                id = 1,
                name = "Updated Group",
                description = "Updated Description",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemGroupService.Setup(service => service.UpdateItem_Groups(itemGroup)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, itemGroup);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContentResult_WhenItemGroupIsDeleted()
        {
            // Arrange
            _mockItemGroupService.Setup(service => service.DeleteItem_Groups(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenItemGroupDoesNotExist()
        {
            // Arrange
            _mockItemGroupService.Setup(service => service.DeleteItem_Groups(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
