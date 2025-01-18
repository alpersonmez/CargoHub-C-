using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllInventories(int amount)
        {
            return await _context.Inventories.Take(amount).ToListAsync(); 
        }
         public async Task<Inventory> GetInventoryById(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }
        public async Task<Inventory> AddInventory(Inventory NewInventory)
        {
            Inventory inventory = new Inventory
            {
                item_id = NewInventory.item_id,
                description = NewInventory.description,
                item_reference = NewInventory.item_reference,
                total_on_hand = NewInventory.total_on_hand,
                total_expected = NewInventory.total_expected,
                total_ordered = NewInventory.total_ordered,
                total_allocated = NewInventory.total_allocated,
                total_available = NewInventory.total_available,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }
        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            Inventory Existinginventory = await _context.Inventories.FindAsync(inventory.id);

            if (Existinginventory == null) return false;

            Existinginventory.description = inventory.description;
            Existinginventory.locations = inventory.locations;
            Existinginventory.total_on_hand = inventory.total_on_hand;
            Existinginventory.total_expected = inventory.total_expected;
            Existinginventory.total_ordered = inventory.total_ordered;
            Existinginventory.total_allocated = inventory.total_allocated;
            Existinginventory.total_available = inventory.total_available;
            Existinginventory.updated_at = DateTime.UtcNow;

            _context.Inventories.Update(Existinginventory);
            _context.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory?.isdeleted == true || inventory == null)
            {
                return false;
            }

            inventory.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}