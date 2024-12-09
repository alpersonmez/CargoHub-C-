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
    public class ClientControllerTests
    {
        private Mock<IClientService> _mockClientService;
        private ClientController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockClientService = new Mock<IClientService>();
            _controller = new ClientController(_mockClientService.Object);
        }

        // Test GetAllClients
        [TestMethod]
        public void GetAllClients_ReturnsOkResult_WithListOfClients()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { Id = 1, Name = "Client A" },
                new Client { Id = 2, Name = "Client B" }
            };
            _mockClientService.Setup(service => service.GetAllClients()).Returns(clients);

            // Act
            var result = _controller.GetAllClients();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(clients, okResult.Value);
        }

        // Test GetClientById - Success
        [TestMethod]
        public void GetClientById_ReturnsOkResult_WithClient()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Client A" };
            _mockClientService.Setup(service => service.GetClientById(1)).Returns(client);

            // Act
            var result = _controller.GetClientById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(client, okResult.Value);
        }

        // Test GetClientById - Not Found
        [TestMethod]
        public void GetClientById_ReturnsNotFound_WhenClientDoesNotExist()
        {
            // Arrange
            _mockClientService.Setup(service => service.GetClientById(It.IsAny<int>())).Returns((Client)null);

            // Act
            var result = _controller.GetClientById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        // Test CreateClient - Success
        [TestMethod]
        public void CreateClient_ReturnsCreatedAtActionResult_WithCreatedClient()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Client A" };
            _mockClientService.Setup(service => service.CreateClient(client)).Returns(client);

            // Act
            var result = _controller.CreateClient(client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(client, createdResult.Value);
        }

        // Test UpdateClient - Success
        [TestMethod]
        public void UpdateClient_ReturnsOkResult_WithUpdatedClient()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Updated Client" };
            _mockClientService.Setup(service => service.UpdateClient(client)).Returns(client);

            // Act
            var result = _controller.UpdateClient(1, client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(client, okResult.Value);
        }

        // Test UpdateClient - Not Found
        [TestMethod]
        public void UpdateClient_ReturnsNotFound_WhenClientDoesNotExist()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Updated Client" };
            _mockClientService.Setup(service => service.UpdateClient(client)).Returns((Client)null);

            // Act
            var result = _controller.UpdateClient(1, client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        // Test DeleteClient - Success
        [TestMethod]
        public void DeleteClient_ReturnsNoContentResult_WhenClientIsDeleted()
        {
            // Arrange
            _mockClientService.Setup(service => service.DeleteClient(1)).Returns(true);

            // Act
            var result = _controller.DeleteClient(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test DeleteClient - Not Found
        [TestMethod]
        public void DeleteClient_ReturnsNotFound_WhenClientDoesNotExist()
        {
            // Arrange
            _mockClientService.Setup(service => service.DeleteClient(1)).Returns(false);

            // Act
            var result = _controller.DeleteClient(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
