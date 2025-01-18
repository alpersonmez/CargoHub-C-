using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly AppDbContext _context;

        public ItemTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemType>> GetAllItemTypes(int amount = 100)
        {
            return await _context.ItemTypes.Take(amount).ToListAsync();
        }

        public async Task<ItemType> GetItemTypeById(int id)
        {
            return await _context.ItemTypes.FindAsync(id);
        }
        public async Task<bool> UpdateItemType(ItemType ItemType)
        {
            ItemType ExistingItemType = await _context.ItemTypes.FindAsync(ItemType.id);

            if (ExistingItemType == null)
            {
                return false;
            }

            ExistingItemType.description = ItemType.description;
            ExistingItemType.name = ItemType.name;
            ExistingItemType.updated_at = DateTime.UtcNow;

            _context.ItemTypes.Update(ExistingItemType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemType(int id)
        {
            var itemType = await _context.ItemTypes.FindAsync(id);
            if (itemType?.isdeleted == true || itemType == null)
            {
                return false;
            }

            itemType.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
