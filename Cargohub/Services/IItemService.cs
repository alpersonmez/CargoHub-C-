using Cargohub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargohub.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems(int amount = 100);
        Task<Item?> GetItemByUid(string uid);
        Task<Item?> GetItemsByItemLineAsync(int itemLineId);
        Task<Item?> GetItemsByItemGroupAsync(int itemGroupId);
        Task<Item?> GetItemsByItemTypeAsync(int itemTypeId);
        Task<Item?> GetItemsBySupplierAsync(int supplierId);
        Task<Item> AddItemAsync(Item newItem);
        Task<bool> UpdateItemAsync(string uid, Item updatedItem);
        Task<bool> RemoveItemAsync(string uid);
    }
}
