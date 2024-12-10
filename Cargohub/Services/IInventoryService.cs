using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IInventoryService
    {
        List<Inventory> GetTotalOfInventories();
        List<Inventory> GetAllInventories();
        Inventory GetInventoryById(int id);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(int id);
        //bool PostInventory(Inventory inventory);
    }
}