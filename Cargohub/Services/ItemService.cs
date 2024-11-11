using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cargohub.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _dbContext;

        public ItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Item> GetAllItems()
        {
            return _dbContext.Items.ToList();
        }

        public Item GetItemByUid(string uid)
        {
            return _dbContext.Items.FirstOrDefault(item => item.Uid == uid);
        }

        public Item CreateItem(Item item)
        {
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public Item UpdateItem(string uid, Item updatedItem)
        {
            var existingItem = GetItemByUid(uid);
            if (existingItem != null)
            {
                _dbContext.Entry(existingItem).CurrentValues.SetValues(updatedItem);
                _dbContext.SaveChanges();
            }
            return updatedItem;
        }

        public void DeleteItem(string uid)
        {
            var item = GetItemByUid(uid);
            if (item != null)
            {
                _dbContext.Items.Remove(item);
                _dbContext.SaveChanges();
            }
        }
    }
}
