using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{

    public class ItemGroupsService : IitemGroupsService
    {
        private readonly AppDbContext _context;

        public ItemGroupsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemGroup>> GetAllItem_Groups()
        {
            return await _context.ItemGroups.Take(100).ToListAsync();
        }

        public async Task<ItemGroup> GetItem_GroupsById(int id)
        {

            return await _context.ItemGroups.FindAsync(id);
        }

        public async Task<bool> UpdateItem_Groups(ItemGroup item_Group)
        {
            ItemGroup existing = await _context.ItemGroups.FindAsync(item_Group.id);

            if (existing == null) return false;

            existing.name = item_Group.name;
            existing.description = item_Group.description;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.ItemGroups.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItem_Groups(int id)
        {

            var item = await _context.ItemGroups.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.ItemGroups.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}