using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        Client CreateClient(Client client);
        Client UpdateClient(Client client);
        bool DeleteClient(int id);
    }
}
