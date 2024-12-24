using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Item> GetItemByUid(string uid);
        Task<Item> AddItem(Item Newitem);
        Task<bool> UpdateItem(Item item);
        Task<bool> DeleteItem(string uid);
    }
}
