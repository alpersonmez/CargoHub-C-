using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetTotalOfInventories()
        {
            return null;
        }
        public async Task<List<Inventory>> GetAllInventories()
        {
            return _context.Inventories.Take(100).ToList();
            //return await _context.Inventories.Take(100).ToListAsync();
        }
        public async Task<Inventory> GetInventoryById(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }
        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            Inventory existing = await _context.Inventories.FindAsync(inventory.id);

            if (existing == null) return false;

            existing.description = inventory.description;
            existing.locations = inventory.locations;
            existing.total_on_hand = inventory.total_on_hand;
            existing.total_expected = inventory.total_expected;
            existing.total_ordered = inventory.total_ordered;
            existing.total_allocated = inventory.total_allocated;
            existing.total_available = inventory.total_available;
            existing.updated_at = DateTime.UtcNow;

            _context.Inventories.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return false;
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}