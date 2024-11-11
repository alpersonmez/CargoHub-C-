using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        Item GetItemByUid(string uid);
        Item CreateItem(Item item);
        Item UpdateItem(string uid, Item item);
        void DeleteItem(string uid);
    }
}
