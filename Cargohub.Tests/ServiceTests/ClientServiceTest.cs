using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Cargohub.Tests
{
    [TestClass]
    public class ClientServiceTests
    {
        private AppDbContext _context;
        private ClientService _clientService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _clientService = new ClientService(_context);

            // Seed the in-memory database
            _context.Clients.AddRange(new List<Client>
            {
                new Client
                {
                    id = 1,
                    name = "Client 1",
                    address = "123 Main St",
                    city = "City A",
                    zip_code = "12345",
                    province = "Province A",
                    country = "Country A",
                    contact_name = "John Doe",
                    contact_phone = "555-1234",
                    contact_email = "john.doe@example.com",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Client
                {
                    id = 2,
                    name = "Client 2",
                    address = "456 Elm St",
                    city = "City B",
                    zip_code = "67890",
                    province = "Province B",
                    country = "Country B",
                    contact_name = "Jane Smith",
                    contact_phone = "555-5678",
                    contact_email = "jane.smith@example.com",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            });
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetAllClients_ReturnsCorrectAmount()
        {
            // Act
            var clients = await _clientService.GetAllClients(2);

            // Assert
            Assert.AreEqual(2, clients.Count);
        }

        [TestMethod]
        public async Task GetClientById_ReturnsCorrectClient()
        {
            // Act
            var client = await _clientService.GetClientById(1);

            // Assert
            Assert.IsNotNull(client);
            Assert.AreEqual("Client 1", client.name);
            Assert.AreEqual("123 Main St", client.address);
            Assert.AreEqual("City A", client.city);
        }

        [TestMethod]
        public async Task GetClientById_ReturnsNull_WhenClientDoesNotExist()
        {
            // Act
            var client = await _clientService.GetClientById(99);

            // Assert
            Assert.IsNull(client);
        }

        [TestMethod]
        public async Task AddClient_AddsClientSuccessfully()
        {
            // Arrange
            var newClient = new Client
            {
                name = "Client 3",
                address = "789 Oak St",
                city = "City C",
                zip_code = "54321",
                province = "Province C",
                country = "Country C",
                contact_name = "Alice Johnson",
                contact_phone = "555-9876",
                contact_email = "alice.johnson@example.com"
            };

            // Act
            var addedClient = await _clientService.AddClient(newClient);

            // Assert
            Assert.IsNotNull(addedClient);
            Assert.AreEqual("Client 3", addedClient.name);
            Assert.AreEqual("789 Oak St", addedClient.address);
            Assert.AreEqual("City C", addedClient.city);
        }

        [TestMethod]
        public async Task UpdateClient_UpdatesExistingClientSuccessfully()
        {
            // Arrange
            var existingClient = await _clientService.GetClientById(1);
            existingClient.city = "Updated City A";

            // Act
            var updated = await _clientService.UpdateClient(existingClient);

            // Assert
            Assert.IsTrue(updated);
            var updatedClient = await _clientService.GetClientById(1);
            Assert.AreEqual("Updated City A", updatedClient.city);
        }

        [TestMethod]
        public async Task UpdateClient_ReturnsFalse_WhenClientDoesNotExist()
        {
            // Arrange
            var nonExistingClient = new Client
            {
                name = "Client 3",
                address = "789 Oak St",
                city = "City C",
                zip_code = "54321",
                province = "Province C",
                country = "Country C",
                contact_name = "Alice Johnson",
                contact_phone = "555-9876",
                contact_email = "alice.johnson@example.com"
            };

            // Act
            var updated = await _clientService.UpdateClient(nonExistingClient);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteClient_SoftDeletesClientSuccessfully()
        {
            // Act
            var deleted = await _clientService.DeleteClient(1);

            // Assert
            Assert.IsTrue(deleted);
            var client = await _clientService.GetClientById(1);
            Assert.IsTrue(client.isdeleted);
        }

        [TestMethod]
        public async Task DeleteClient_ReturnsFalse_WhenClientDoesNotExist()
        {
            // Act
            var deleted = await _clientService.DeleteClient(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
