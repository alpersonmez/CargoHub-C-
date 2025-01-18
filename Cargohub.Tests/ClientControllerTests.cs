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

        [TestMethod]
        public async Task TestGetAllClients()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    id = 1,
                    name = "Client A",
                    address = "123 Street",
                    city = "City A",
                    zip_code = "12345",
                    province = "Province A",
                    country = "Country A",
                    contact_name = "John Doe",
                    contact_phone = "1234567890",
                    contact_email = "john.doe@example.com",
                    created_at = DateTime.UtcNow.AddDays(-5),
                    updated_at = DateTime.UtcNow,
                    isdeleted = false

                },
                new Client
                {
                    id = 2,
                    name = "Client B",
                    address = "456 Avenue",
                    city = "City B",
                    zip_code = "67890",
                    province = "Province B",
                    country = "Country B",
                    contact_name = "Jane Smith",
                    contact_phone = "0987654321",
                    contact_email = "jane.smith@example.com",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow,
                    isdeleted = false
                }
            };
            _mockClientService.Setup(service => service.GetAllClients(It.IsAny<int>())).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetAllClients(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedClients = okResult.Value as List<Client>;
            Assert.IsNotNull(returnedClients);
            Assert.AreEqual(2, returnedClients.Count);
            Assert.AreEqual("Client A", returnedClients[0].name);
            Assert.AreEqual("Client B", returnedClients[1].name);
        }

        [TestMethod]
        public async Task TestGetClientById()
        {
            // Arrange
            var client = new Client
            {
                id = 1,
                name = "Client A",
                address = "123 Street",
                city = "City A",
                zip_code = "12345",
                province = "Province A",
                country = "Country A",
                contact_name = "John Doe",
                contact_phone = "1234567890",
                contact_email = "john.doe@example.com",
                created_at = DateTime.UtcNow.AddDays(-5),
                updated_at = DateTime.UtcNow,
                isdeleted = false
            };
            _mockClientService.Setup(service => service.GetClientById(1)).ReturnsAsync(client);

            // Act
            var result = await _controller.GetClientById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedClient = okResult.Value as Client;
            Assert.IsNotNull(returnedClient);
            Assert.AreEqual(client.id, returnedClient.id);
            Assert.AreEqual(client.name, returnedClient.name);
        }

        [TestMethod]
        public async Task TestGetClientByIdNotFound()
        {
            // Arrange
            _mockClientService.Setup(service => service.GetClientById(It.IsAny<int>())).ReturnsAsync((Client)null);

            // Act
            var result = await _controller.GetClientById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task TestCreateClient()
        {
            // Arrange
            var client = new Client
            {
                name = "Client A",
                address = "123 Street",
                city = "City",
                zip_code = "12345",
                province = "Province",
                country = "Country",
                contact_name = "John Doe",
                contact_phone = "1234567890",
                contact_email = "john.doe@example.com"
            };
            _mockClientService.Setup(service => service.AddClient(client)).ReturnsAsync(client);

            // Act
            var result = await _controller.CreateClient(client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(client, createdResult.Value);
        }

        [TestMethod]
        public async Task TestCreateClientInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("name", "Name is required");

            // Act
            var result = await _controller.CreateClient(new Client());

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TestUpdateClient()
        {
            // Arrange
            var client = new Client
            {
                id = 1,
                name = "Updated Client",
                address = "456 Avenue",
                city = "Updated City",
                zip_code = "67890",
                province = "Updated Province",
                country = "Updated Country",
                contact_name = "Jane Doe",
                contact_phone = "0987654321",
                contact_email = "jane.doe@example.com",
            };
            _mockClientService.Setup(service => service.UpdateClient(client)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestUpdateClientNotFound()
        {
            // Arrange
            var client = new Client
            {
                id = 1,
                name = "Updated Client",
                address = "456 Avenue",
                city = "Updated City",
                zip_code = "67890",
                province = "Updated Province",
                country = "Updated Country",
                contact_name = "Jane Doe",
                contact_phone = "0987654321",
                contact_email = "jane.doe@example.com",
            };
            _mockClientService.Setup(service => service.UpdateClient(client)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestDeleteClient()
        {
            // Arrange
            _mockClientService.Setup(service => service.DeleteClient(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDeleteClientNotFound()
        {
            // Arrange
            _mockClientService.Setup(service => service.DeleteClient(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
