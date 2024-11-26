using System.Collections.Generic;
using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IItemTypeService
    {
        List<ItemType> GetAllItemTypes();
        ItemType GetItemTypeById(int id);
        ItemType CreateItemType(ItemType itemType);
        ItemType UpdateItemType(int id, ItemType updatedItemType);
        bool DeleteItemType(int id);
    }
}
