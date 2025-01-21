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
    public class TransferControllerTests
    {
        private Mock<ITransferService> _mockTransferService;
        private TransferController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockTransferService = new Mock<ITransferService>();
            _controller = new TransferController(_mockTransferService.Object);
        }

        [TestMethod]
        public async Task GetAllTransfers_ReturnsOkResult_WithListOfTransfers()
        {
            // Arrange
            var transfers = new List<Transfer>
            {
                new Transfer
                {
                    id = 1,
                    reference = "TR001",
                    transfer_from = 100,
                    transfer_to = 200,
                    transfer_status = "Complete",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Transfer
                {
                    id = 2,
                    reference = "TR002",
                    transfer_from = 300,
                    transfer_to = 400,
                    transfer_status = "Pending",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-15),
                    isdeleted = false
                }
            };
            _mockTransferService.Setup(service => service.GetTransfers(It.IsAny<int>())).ReturnsAsync(transfers);

            // Act
            var result = await _controller.GetTransfers(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transfers, okResult.Value);
        }

        [TestMethod]
        public async Task GetTransferById_ReturnsOkResult_WithTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                reference = "TR001",
                transfer_from = 100,
                transfer_to = 200,
                transfer_status = "Complete",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockTransferService.Setup(service => service.GetTransfer(1)).ReturnsAsync(transfer);

            // Act
            var result = await _controller.GetTransfer(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transfer, okResult.Value);
        }

        [TestMethod]
        public async Task GetTransferById_ReturnsNotFound_WhenTransferDoesNotExist()
        {
            // Arrange
            _mockTransferService.Setup(service => service.GetTransfer(It.IsAny<int>())).ReturnsAsync((Transfer)null);

            // Act
            var result = await _controller.GetTransfer(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task CreateTransfer_ReturnsCreatedAtActionResult_WithCreatedTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                reference = "TR001",
                transfer_from = 100,
                transfer_to = 200,
                transfer_status = "Complete",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockTransferService.Setup(service => service.AddTransfer(transfer)).ReturnsAsync(transfer);

            // Act
            var result = await _controller.AddTransfer(transfer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(transfer, createdResult.Value);
        }

        [TestMethod]
        public async Task UpdateTransfer_ReturnsOkResult_WithUpdatedTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                reference = "TR001",
                transfer_from = 100,
                transfer_to = 200,
                transfer_status = "Complete",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockTransferService.Setup(service => service.UpdateTransfer(transfer)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateTransfer(1, transfer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transfer, okResult.Value);
        }

        [TestMethod]
        public async Task UpdateTransfer_ReturnsNotFound_WhenTransferDoesNotExist()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                reference = "TR001",
                transfer_from = 100,
                transfer_to = 200,
                transfer_status = "Complete",
                created_at = DateTime.UtcNow.AddDays(-10),
                updated_at = DateTime.UtcNow.AddDays(-5),
                isdeleted = false
            };
            _mockTransferService.Setup(service => service.UpdateTransfer(transfer)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateTransfer(1, transfer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteTransfer_ReturnsNoContentResult_WhenTransferIsDeleted()
        {
            // Arrange
            _mockTransferService.Setup(service => service.DeleteTransfer(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTransfer(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteTransfer_ReturnsNotFound_WhenTransferDoesNotExist()
        {
            // Arrange
            _mockTransferService.Setup(service => service.DeleteTransfer(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteTransfer(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
