using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllClients(int amount = 100)
        {
            return await _context.Clients.Take(amount).ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> AddClient(Client newClient)
        {
            Client client = new Client
            {
                name = newClient.name,
                address = newClient.address,
                city = newClient.city,
                zip_code = newClient.zip_code,
                province = newClient.province,
                country = newClient.country,
                contact_name = newClient.contact_name,
                contact_phone = newClient.contact_phone,
                contact_email = newClient.contact_email,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> UpdateClient(Client client)
        {
            Client existingClient = await _context.Clients.FindAsync(client.id);

            if (existingClient == null)
            {
                return false;
            }

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

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client?.isdeleted == true || client == null)
            {
                return false;
            }

            client.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
