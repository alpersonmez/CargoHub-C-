using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetTotalOfInventories();
        Task<List<Inventory>> GetAllInventories();
        Task<Inventory> GetInventoryById(int id);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> DeleteInventory(int id);
    }
}