using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cargohub.Models;


namespace Cargohub.Services
{

    public class ItemGroupService : IItemGroupService
    {
        private readonly AppDbContext _context;

        public ItemGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemGroup>>? GetAllItem_Groups()
        {
            return await _context.ItemGroups.Take(100).ToListAsync();
        }

        public async Task<ItemGroup>? GetItem_GroupById(int id)
        {
            ItemGroup? doesExist = await _context.ItemGroups.FirstOrDefaultAsync(x => x.id == id);
            Console.WriteLine("got here");
            if (doesExist == null) return null;


            return doesExist;
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
            ItemGroup? item = await _context.ItemGroups.FindAsync(id);
            if (item == null) return false;
            _context.ItemGroups.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostItemGroup(ItemGroup itemGroup)
        {
            ItemGroup? exists = await _context.ItemGroups.FindAsync(itemGroup.id);
            if (exists != null) return false;
            DateTime now = DateTime.Now;
            itemGroup.CreatedAt = now;
            itemGroup.UpdatedAt = now;
            _context.ItemGroups.Add(itemGroup);
            int saved = await _context.SaveChangesAsync();
            if (saved == 1) return true;
            return false;
        }
    }
}