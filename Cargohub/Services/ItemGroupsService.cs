using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cargohub.Services
{
    public class ItemGroupService : IItemGroupService
    {
        private readonly AppDbContext _context;

        public ItemGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemGroup>> GetAllItemGroups(int amount)
        {
            return await _context.ItemGroups.Take(amount).ToListAsync();
        }

        public async Task<ItemGroup>? GetItemGroupById(int id)
        {
            return await _context.ItemGroups.FindAsync(id);
        }

        public async Task<bool> UpdateItem_Groups(ItemGroup item_Group)
        {
            ItemGroup existing = await _context.ItemGroups.FindAsync(item_Group.id);

            if (existing == null) return false;

            existing.name = item_Group.name;
            existing.description = item_Group.description;
            existing.updated_at = DateTime.UtcNow;

            _context.ItemGroups.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItem_Groups(int id)
        {
            var itemgroup = await _context.ItemGroups.FindAsync(id);
            if (itemgroup?.isdeleted == true || itemgroup == null)
            {
                return false;
            }

            itemgroup.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}