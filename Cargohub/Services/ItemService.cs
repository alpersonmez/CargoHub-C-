using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            //return _dbContext.Items.ToList();
            return  _dbContext.Items
                .Include(i => i.item_line)
                .Include(i => i.item_group)
                .Include(i => i.item_type)
                //.Include(i => i.supplier) werkt nog niet
                .OrderBy(i => i.uid) // Order by Id in ascending order
                .Take(10)
                .ToList();
            
        }

        public Item GetItemByUid(string uid)
        {
            return _dbContext.Items.FirstOrDefault(item => item.uid == uid);
        }

        public Item CreateItem(Item item)
        {
            // Automatically set createdAt and updatedAt
            item.created_at = DateTime.UtcNow;
            item.updated_at = DateTime.UtcNow;

            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
            return item;
        }


       public Item UpdateItem(string uid, Item updatedItem)
        {
            var existingItem = _dbContext.Items.SingleOrDefault(item => item.uid == uid);
            if (existingItem == null) return null;

            // Update fields except createdAt
            existingItem.code = updatedItem.code;
            existingItem.description = updatedItem.description;
            existingItem.short_description = updatedItem.short_description;
            existingItem.upc_code = updatedItem.upc_code;
            existingItem.model_number = updatedItem.model_number;
            existingItem.commodity_code = updatedItem.commodity_code;
            existingItem.item_line = updatedItem.item_line;
            existingItem.item_group = updatedItem.item_group;
            existingItem.item_type = updatedItem.item_type;
            existingItem.unit_purchase_quantity = updatedItem.unit_purchase_quantity;
            existingItem.unit_order_quantity = updatedItem.unit_order_quantity;
            existingItem.pack_order_quantity = updatedItem.pack_order_quantity;
            existingItem.supplier_id = updatedItem.supplier_id;
            existingItem.supplier_code = updatedItem.supplier_code;
            existingItem.supplier_part_number = updatedItem.supplier_part_number;
            existingItem.updated_at = DateTime.UtcNow; // Update updatedAt timestamp

            _dbContext.SaveChanges();
            return existingItem;
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
