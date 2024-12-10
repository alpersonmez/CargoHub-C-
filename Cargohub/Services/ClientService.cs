using System;
using System.Collections.Generic;
using System.Linq;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _dbContext;

        public ClientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all clients
        public IEnumerable<Client> GetAllClients()
        {
            return _dbContext.Clients.ToList();
        }

        // Get a client by ID
        public Client GetClientById(int id)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.Id == id);
        }

        // Create a new client
        public Client CreateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.CreatedAt = DateTime.UtcNow;
            client.UpdatedAt = DateTime.UtcNow;

            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            return client;
        }

        // Update an existing client
        public Client UpdateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var existingClient = _dbContext.Clients.FirstOrDefault(c => c.Id == client.Id);
            if (existingClient == null) return null;

            // Update fields
            existingClient.Name = client.Name;
            existingClient.Address = client.Address;
            existingClient.City = client.City;
            existingClient.ZipCode = client.ZipCode;
            existingClient.Province = client.Province;
            existingClient.Country = client.Country;
            existingClient.ContactName = client.ContactName;
            existingClient.ContactPhone = client.ContactPhone;
            existingClient.ContactEmail = client.ContactEmail;
            existingClient.UpdatedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return existingClient;
        }

        // Delete a client
        public bool DeleteClient(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null) return false;

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
