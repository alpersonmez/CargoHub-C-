using Microsoft.EntityFrameworkCore;
using  Cargohub.Models;

namespace Cargohub.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _context;

        public ItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllItems()
        {
            //return _dbContext.Items.ToList();
            return  await _context.Items
                .Include(i => i.ItemLine)
                .Include(i => i.ItemGroup)
                .Include(i => i.ItemGroup)
                //.Include(i => i.supplier) werkt nog niet
                .OrderBy(i => i.uid) // Order by Id in ascending order
                .Take(100)
                .ToListAsync();
            
        }

        public async Task<Item> GetItemByUid(string uid)
        {
            return await _context.Items.FindAsync(uid);
        }

        public async Task<Item> AddItem(Item NewItem)
        {
            // Automatically set createdAt and updatedAt
        Item item = new Item
        { 
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }


       public async Task<bool> UpdateItem(Item item)
        {
            Item existingItem = await _context.Items.FindAsync(item.uid);
            if (existingItem == null) return false;

            // Update fields except createdAt
            existingItem.code = item.code;
            existingItem.description = item.description;
            existingItem.short_description = item.short_description;
            existingItem.upc_code = item.upc_code;
            existingItem.model_number = item.model_number;
            existingItem.commodity_code = item.commodity_code;
            existingItem.ItemLine = item.ItemLine;
            existingItem.ItemGroup = item.ItemGroup;
            existingItem.ItemType = item.ItemType;
            existingItem.unit_purchase_quantity = item.unit_purchase_quantity;
            existingItem.unit_order_quantity = item.unit_order_quantity;
            existingItem.pack_order_quantity = item.pack_order_quantity;
            existingItem.supplier_id = item.supplier_id;
            existingItem.supplier_code = item.supplier_code;
            existingItem.supplier_part_number = item.supplier_part_number;
            existingItem.updated_at = DateTime.UtcNow; // Update updatedAt timestamp

            _context.Items.Update(existingItem);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteItem(string uid)
        {
            var item = await _context.Items.FindAsync(uid);
            if (item == null)
            {
                return false;
            }
            
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
