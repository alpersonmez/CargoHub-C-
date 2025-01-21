using Microsoft.EntityFrameworkCore;
using Cargohub.Models;
using System.Text.RegularExpressions;

namespace Cargohub.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllClients(int amount)
        {
            return await _context.Clients.Take(amount).ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> AddClient(Client newClient)
        {
            var validationErrors = ValidateClient(newClient);
            if (validationErrors.Any())
            {
                throw new ArgumentException($"Validation errors: {string.Join(" ", validationErrors)}");
            }

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
            var validationErrors = ValidateClient(client);
            if (validationErrors.Any())
            {
                throw new ArgumentException($"Validation errors: {string.Join(" ", validationErrors)}");
            }

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

        public async Task<List<Order>> GetClientOrders(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client?.isdeleted == true || client == null)
            {
                return null;
            }
            return _context.Orders.Where(x => x.ship_to == client.id.ToString() || x.bill_to == client.id.ToString()).ToList();
        }

        private List<string> ValidateClient(Client client)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(client.name))
            {
                errors.Add("The name field is required.");
            }
            if (string.IsNullOrWhiteSpace(client.address))
            {
                errors.Add("The address field is required.");
            }
            if (string.IsNullOrWhiteSpace(client.contact_name))
            {
                errors.Add("The contact_name field is required.");
            }
            if (string.IsNullOrWhiteSpace(client.contact_phone))
            {
                errors.Add("The contact_phone field is required.");
            }
            if (string.IsNullOrWhiteSpace(client.contact_email) || !IsValidEmail(client.contact_email))
            {
                errors.Add("The contact_email field is required and must be a valid email.");
            }

            return errors;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
