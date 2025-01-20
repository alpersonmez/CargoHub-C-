using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients(int amount);
        Task<Client> GetClientById(int id);
        Task<Client> AddClient(Client newClient);
        Task<bool> UpdateClient(Client client);
        Task<bool> DeleteClient(int id);
        Task<List<Order>> GetClientOrders(int id);
    }
}
