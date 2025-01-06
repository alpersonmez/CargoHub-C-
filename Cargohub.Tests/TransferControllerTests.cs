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

            };
            _mockTransferService.Setup(service => service.GetAllTransfers(100)).ReturnsAsync(transfers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transfers, okResult.Value);
        }

//         // Test GetTransferById - Success
//         [TestMethod]
//         public async Task GetTransferById_ReturnsOkResult_WithTransfer()
//         {
//             // Arrange
//             var transfer = new Transfer
//             {
//                 id = 1,
//                 source_id = 33,
//                 transfer_date = DateTime.Parse("2019-04-03T11:33:15Z"),
//                 request_date = DateTime.Parse("2019-04-07T11:33:15Z"),
//                 reference = "ORD00001",
//                 reference_extra = "Bedreven arm straffen bureau.",
//                 transfer_status = "Delivered",
//                 notes = "Voedsel vijf vork heel.",
//                 shipping_notes = "Buurman betalen plaats bewolkt.",
//                 picking_notes = "Ademen fijn volgorde scherp aardappel op leren.",
//                 warehouse_id = 18,
//                 ship_to = "Duitsland",
//                 bill_to = "Nederland",
//                 shipment_id = 1
//             };
//             _mockTransferService.Setup(service => service.GetTransferById(1)).ReturnsAsync(transfer);

//             // Act
//             var result = await _controller.Get(1);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//             var okResult = result as OkObjectResult;
//             Assert.IsNotNull(okResult);
//             Assert.AreEqual(transfer, okResult.Value);
//         }

//         // Test GetTransferById - Not Found
//         [TestMethod]
//         public async Task GetTransferById_ReturnsNotFound_WhenTransferDoesNotExist()
//         {
//             // Arrange
//             _mockTransferService.Setup(service => service.GetTransferById(It.IsAny<int>())).ReturnsAsync((Transfer)null);

//             // Act
//             var result = await _controller.Get(1);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//         }

//         // Test CreateTransfer - Success
//         [TestMethod]
//         public async Task CreateTransfer_ReturnsCreatedAtActionResult_WithCreatedTransfer()
//         {
//             // Arrange
//             var transfer = new Transfer
//             {
//                 id = 1,
//                 source_id = 33,
//                 transfer_date = DateTime.Parse("2019-04-03T11:33:15Z"),
//                 request_date = DateTime.Parse("2019-04-07T11:33:15Z"),
//                 reference = "ORD00001",
//                 reference_extra = "Bedreven arm straffen bureau.",
//                 transfer_status = "Delivered",
//                 notes = "Voedsel vijf vork heel.",
//                 shipping_notes = "Buurman betalen plaats bewolkt.",
//                 picking_notes = "Ademen fijn volgorde scherp aardappel op leren.",
//                 warehouse_id = 18,
//                 ship_to = "Duitsland",
//                 bill_to = "Nederland",
//                 shipment_id = 1
//             };
//             _mockTransferService.Setup(service => service.AddTransfer(transfer)).ReturnsAsync(transfer);

//             // Act
//             var result = await _controller.Create(transfer);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
//             var createdResult = result as CreatedAtActionResult;
//             Assert.IsNotNull(createdResult);
//             Assert.AreEqual(transfer, createdResult.Value);
//         }

//         // Test UpdateTransfer - Success
//         [TestMethod]
//         public async Task UpdateTransfer_ReturnsOkResult_WithUpdatedTransfer()
//         {
//             // Arrange
//             var transfer = new Transfer { id = 1, notes = "Updated Transfer" };
//             _mockTransferService.Setup(service => service.UpdateTransfer(transfer)).ReturnsAsync(true);

//             // Act
//             var result = await _controller.Update(1, transfer);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//             var okResult = result as OkObjectResult;
//             Assert.IsNotNull(okResult);
//             Assert.AreEqual(transfer, okResult.Value);
//         }

//         // Test UpdateTransfer - Not Found
//         [TestMethod]
//         public async Task UpdateTransfer_ReturnsNotFound_WhenTransferDoesNotExist()
//         {
//             // Arrange
//             var transfer = new Transfer { id = 1, notes = "Updated Transfer" };
//             _mockTransferService.Setup(service => service.UpdateTransfer(transfer)).ReturnsAsync(false);

//             // Act
//             var result = await _controller.Update(1, transfer);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//         }

//         // Test DeleteTransfer - Success
//         [TestMethod]
//         public async Task DeleteTransfer_ReturnsNoContentResult_WhenTransferIsDeleted()
//         {
//             // Arrange
//             _mockTransferService.Setup(service => service.DeleteTransfer(1)).ReturnsAsync(true);

//             // Act
//             var result = await _controller.Delete(1);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(NoContentResult));
//         }

//         // Test DeleteTransfer - Not Found
//         [TestMethod]
//         public async Task DeleteTransfer_ReturnsNotFound_WhenTransferDoesNotExist()
//         {
//             // Arrange
//             _mockTransferService.Setup(service => service.DeleteTransfer(1)).ReturnsAsync(false);

//             // Act
//             var result = await _controller.Delete(1);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//         }
//     }
// }