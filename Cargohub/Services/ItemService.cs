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

        public async Task<List<Item>> GetAllItems(int amount = 100)
        {
            //return _dbContext.Items.ToList();
            return  await _context.Items
                .Include(i => i.ItemLine)
                .Include(i => i.ItemGroup)
                .Include(i => i.ItemGroup)
                .Include(i => i.supplier) 
                //.OrderBy(i => i.uid) // Order by Id in ascending order
                .Take(amount)
                .ToListAsync();
            
        }

        public async Task<Item> GetItemByUid(string uid)
        {
            return await _context.Items
                .Include(i => i.ItemLine)
                .Include(i => i.ItemGroup)
                .Include(i => i.ItemType)
                .Include(i => i.supplier)
                .FirstOrDefaultAsync(i => i.uid == uid);
            
            //return await _context.Items.FindAsync(uid);
        }

        public async Task<Item?> GetItemsByItemLineAsync(int itemLineId)
        {
            return await _context.Items
                .Include(i => i.ItemLine)
                .FirstOrDefaultAsync(i => i.ItemLine.id == itemLineId);

            // .Where(i => i.ItemLineId == itemLineId)
            // .Include(i => i.ItemLine)
            // .ToListAsync();
        }

        public async Task<Item?> GetItemsByItemGroupAsync(int itemGroupId)
        {
            return await _context.Items
                .Include(i => i.ItemGroup)
                .FirstOrDefaultAsync(i => i.ItemGroup.id == itemGroupId);

            // .Where(i => i.ItemGroupId == itemGroupId)
            // .Include(i => i.ItemGroup)
            // .ToListAsync();
        }

        public async Task<Item?> GetItemsByItemTypeAsync(int itemTypeId)
        {
            return await _context.Items
            .Include(i => i.ItemType)
            .FirstOrDefaultAsync(i => i.ItemType.id == itemTypeId);
            // .Where(i => i.ItemTypeId == itemTypeId)
            // .Include(i => i.ItemType)
            // .ToListAsync();
        }

        public async Task<Item?> GetItemsBySupplierAsync(int supplierId)
        {
            return await _context.Items
                .Include(i => i.supplier)
                .FirstOrDefaultAsync(i => i.supplier.id == supplierId);
        }

        public async Task<Item> AddItemAsync(Item newItem)
        {
            // Get the latest UID
            var lastItem = await _context.Items
                .OrderByDescending(i => i.uid)
                .FirstOrDefaultAsync();

            // Generate UID (increment from last UID)
            if (lastItem != null)
            {
                var lastUidNumericPart = int.Parse(lastItem.uid.Substring(1)); // Remove 'P' and parse number
                newItem.uid = $"P{lastUidNumericPart + 1:D6}"; // Increment and format as P###### (e.g., P000002)
            }
            else
            {
                newItem.uid = "P000001"; // First UID
            }

            // Generate Code (random alphanumeric string)
            //newItem.code = GenerateUniqueCode();

            DateTime created_at = DateTime.UtcNow;
            DateTime updated_at = DateTime.UtcNow;

            newItem.created_at = new DateTime(created_at.Year, created_at.Month, created_at.Day, created_at.Hour, created_at.Minute, created_at.Second, DateTimeKind.Utc);
            newItem.updated_at = new DateTime(updated_at.Year, updated_at.Month, updated_at.Day, updated_at.Hour, updated_at.Minute, updated_at.Second, DateTimeKind.Utc);

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }


        public async Task<bool> UpdateItemAsync(string uid, Item updatedItem)
        {
            var existingItem = await _context.Items.FindAsync(uid);

            if (existingItem == null)
            {
                return false;
            }

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

            existingItem.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(string uid)
        {
            var item = await _context.Items.FindAsync(uid);
            if (item?.isdeleted == true || item == null)
            {
                return false;
            }

            item.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
