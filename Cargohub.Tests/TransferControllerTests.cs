using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Controllers;
using Cargohub.Models;
using Cargohub.Services;
using System.Collections.Generic;


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

        // Test GetAllTransfers
        [TestMethod]
        public async Task GetAllTransfers_ReturnsOkResult_WithListOfTransfers()
        {
            // Arrange
            var transfers = new List<Transfer>
            {
                new Transfer{
                    id = 1,
                    transfer_status = "complete"
                },
                new Transfer{
                    id = 2,
                    transfer_status = "complete"
                }
            };
            _mockTransferService.Setup(service => service.GetTransfers(100)).ReturnsAsync(transfers);

            // Act
            var result = await _controller.GetTransfers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transfers, okResult.Value);
        }

        // Test GetTransferById - Success
        [TestMethod]
        public async Task GetTransferById_ReturnsOkResult_WithTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                transfer_status = "complete"
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

        // Test GetTransferById - Not Found
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

        // Test CreateTransfer - Success
        [TestMethod]
        public async Task CreateTransfer_ReturnsCreatedAtActionResult_WithCreatedTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                transfer_status = "complete"
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

        // Test UpdateTransfer - Success
        [TestMethod]
        public async Task UpdateTransfer_ReturnsOkResult_WithUpdatedTransfer()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                transfer_status = "complete"
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

        // Test UpdateTransfer - Not Found
        [TestMethod]
        public async Task UpdateTransfer_ReturnsNotFound_WhenTransferDoesNotExist()
        {
            // Arrange
            var transfer = new Transfer
            {
                id = 1,
                transfer_status = "complete"
            };
            _mockTransferService.Setup(service => service.UpdateTransfer(transfer)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateTransfer(1, transfer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test DeleteTransfer - Success
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

        // Test DeleteTransfer - Not Found
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