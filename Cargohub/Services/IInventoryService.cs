using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IinventoryService
    {
        public Task<List<Inventory>> GetAllInventories(int amount = 100);
        public Task<Inventory?> GetInventoryById(int id);
        public Task<int> PostInventory(Inventory inventory);
        public Task<int> UpdateInventory(int id, Inventory inventory);
        public Task<bool> DeleteInventory(int id);
    }
}