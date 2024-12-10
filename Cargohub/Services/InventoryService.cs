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

        public List<Inventory> GetTotalOfInventories()
        {
            return _context.Inventories.Take(100).ToList();
        }
        public List<Inventory> GetAllInventories()
        {
            return _context.Inventories.Take(100).ToList();
            //return await _context.Inventories.Take(100).ToListAsync();
        }
        public Inventory GetInventoryById(int id)
        {
            return _context.Inventories.Find(id);
        }
        public bool UpdateInventory(Inventory inventory)
        {
            Inventory? existing = _context.Inventories.Find(inventory.id);

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
            _context.SaveChanges();
            return true;
        }
        public bool DeleteInventory(int id)
        {
            var inventory = _context.Inventories.Find(id);
            if (inventory == null)
            {
                return false;
            }

            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
            return true;
        }
    }
}