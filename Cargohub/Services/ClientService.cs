using System;
using System.Collections.Generic;
using System.Linq;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ClientService : IClientService
    {
        private readonly List<Client> _clients = new List<Client>();

        public IEnumerable<Client> GetAllClients()
        {
            return _clients;
        }

        public Client GetClientById(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public Client CreateClient(Client client)
        {
            client.Id = _clients.Any() ? _clients.Max(c => c.Id) + 1 : 1;
            client.CreatedAt = DateTime.UtcNow;
            client.UpdatedAt = DateTime.UtcNow;
            _clients.Add(client);
            return client;
        }

        public Client UpdateClient(Client client)
        {
            var existingClient = GetClientById(client.Id);
            if (existingClient == null)
                return null;

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

            return existingClient;
        }

        public bool DeleteClient(int id)
        {
            var client = GetClientById(id);
            if (client == null)
                return false;

            _clients.Remove(client);
            return true;
        }
    }
}
