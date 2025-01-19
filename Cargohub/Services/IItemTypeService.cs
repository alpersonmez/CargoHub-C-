using System.Collections.Generic;
using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IItemTypeService
    {
        Task<List<ItemType>> GetAllItemTypes(int amount);
        Task<ItemType> GetItemTypeById(int id);
        Task<bool> UpdateItemType(ItemType itemType);
        Task<bool> DeleteItemType(int id);
    }
}
