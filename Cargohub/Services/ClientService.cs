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
            return _dbContext.Clients.FirstOrDefault(c => c.id == id);
        }

        // Create a new client
        public Client CreateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.created_at = DateTime.UtcNow;
            client.updated_at = DateTime.UtcNow;

            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            return client;
        }

        // Update an existing client
        public Client UpdateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var existingClient = _dbContext.Clients.FirstOrDefault(c => c.id == client.id);
            if (existingClient == null) return null;

            // Update fields
            existingClient.name = client.name;
            existingClient.address = client.address;
            existingClient.city = client.city;
            existingClient.zip_code = client.zip_code;
            existingClient.province = client.province;
            existingClient.country = client.country;
            existingClient.contact_name = client.contact_name;
            existingClient.contact_phone = client.contact_phone;
            existingClient.contact_email = client.contact_email;
            existingClient.updated_at = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return existingClient;
        }

        // Delete a client
        public bool DeleteClient(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.id == id);
            if (client == null) return false;

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
