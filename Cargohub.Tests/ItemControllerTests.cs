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
    public class ItemControllerTests
    {
        private Mock<IItemService> _mockItemService;
        private ItemController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockItemService = new Mock<IItemService>();
            _controller = new ItemController(_mockItemService.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfItems()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item
                {
                    id = 1,
                    uid = "item1",
                    code = "CODE1",
                    description = "Item 1 Description",
                    short_description = "Short Desc 1",
                    upc_code = "UPC1",
                    model_number = "Model1",
                    commodity_code = "CommCode1",
                    item_line = 1,
                    item_group = 1,
                    item_type = 1,
                    unit_purchase_quantity = 10,
                    unit_order_quantity = 5,
                    pack_order_quantity = 2,
                    supplier_id = 1,
                    supplier_code = "SUPP1",
                    supplier_part_number = "PART1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Item
                {
                    id = 2,
                    uid = "item2",
                    code = "CODE2",
                    description = "Item 2 Description",
                    short_description = "Short Desc 2",
                    upc_code = "UPC2",
                    model_number = "Model2",
                    commodity_code = "CommCode2",
                    item_line = 2,
                    item_group = 2,
                    item_type = 2,
                    unit_purchase_quantity = 20,
                    unit_order_quantity = 10,
                    pack_order_quantity = 5,
                    supplier_id = 2,
                    supplier_code = "SUPP2",
                    supplier_part_number = "PART2",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            };
            _mockItemService.Setup(service => service.GetAllItems(It.IsAny<int>())).ReturnsAsync(items);

            // Act
            var result = await _controller.GetAll(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(items, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithItem()
        {
            // Arrange
            var item = new Item
            {
                id = 1,
                uid = "item1",
                code = "CODE1",
                description = "Item 1 Description",
                short_description = "Short Desc 1",
                upc_code = "UPC1",
                model_number = "Model1",
                commodity_code = "CommCode1",
                item_line = 1,
                item_group = 1,
                item_type = 1,
                unit_purchase_quantity = 10,
                unit_order_quantity = 5,
                pack_order_quantity = 2,
                supplier_id = 1,
                supplier_code = "SUPP1",
                supplier_part_number = "PART1",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockItemService.Setup(service => service.GetItemByUid("item1")).ReturnsAsync(item);

            // Act
            var result = await _controller.Get("item1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(item, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockItemService.Setup(service => service.GetItemByUid(It.IsAny<string>())).ReturnsAsync((Item)null);

            // Act
            var result = await _controller.Get("nonexistent");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_ReturnsCreatedAtActionResult_WithCreatedItem()
        {
            // Arrange
            var newItem = new Item
            {
                uid = "item3",
                code = "CODE3",
                description = "New Item Description",
                short_description = "Short Desc 3",
                upc_code = "UPC3",
                model_number = "Model3",
                commodity_code = "CommCode3",
                item_line = 3,
                item_group = 3,
                item_type = 3,
                unit_purchase_quantity = 15,
                unit_order_quantity = 7,
                pack_order_quantity = 3,
                supplier_id = 3,
                supplier_code = "SUPP3",
                supplier_part_number = "PART3",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemService.Setup(service => service.AddItemAsync(newItem)).ReturnsAsync(newItem);

            // Act
            var result = await _controller.Create(newItem);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(newItem, createdResult.Value);
        }

        [TestMethod]
        public async Task Update_ReturnsOkResult_WithUpdatedItem()
        {
            // Arrange
            var updatedItem = new Item
            {
                uid = "item1",
                code = "CODE1",
                description = "Updated Item Description",
                short_description = "Short Desc Updated",
                upc_code = "UPC1",
                model_number = "Model1",
                commodity_code = "CommCode1",
                item_line = 1,
                item_group = 1,
                item_type = 1,
                unit_purchase_quantity = 12,
                unit_order_quantity = 6,
                pack_order_quantity = 2,
                supplier_id = 1,
                supplier_code = "SUPP1",
                supplier_part_number = "PART1",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemService.Setup(service => service.UpdateItemAsync("item1", updatedItem)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update("item1", updatedItem);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedItem, okResult.Value);
        }

        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var updatedItem = new Item
            {
                uid = "item1",
                code = "CODE1",
                description = "Updated Item Description",
                short_description = "Short Desc Updated",
                upc_code = "UPC1",
                model_number = "Model1",
                commodity_code = "CommCode1",
                item_line = 1,
                item_group = 1,
                item_type = 1,
                unit_purchase_quantity = 12,
                unit_order_quantity = 6,
                pack_order_quantity = 2,
                supplier_id = 1,
                supplier_code = "SUPP1",
                supplier_part_number = "PART1",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockItemService.Setup(service => service.UpdateItemAsync("item1", updatedItem)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update("item1", updatedItem);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContentResult_WhenItemIsDeleted()
        {
            // Arrange
            _mockItemService.Setup(service => service.RemoveItemAsync("item1")).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("item1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockItemService.Setup(service => service.RemoveItemAsync("item1")).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete("item1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
