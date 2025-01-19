using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetAllInventories(int amount);
        Task<Inventory> GetInventoryById(int id);
        Task<Inventory> AddInventory(Inventory inventory);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> DeleteInventory(int id);
    }
}