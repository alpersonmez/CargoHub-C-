using Cargohub.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cargohub.Services
{
    public class InventoryService : IinventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllInventories(int amount = 100)
        {
            return await _context.Inventories.Where(i => i.isdeleted == false).Take(amount).ToListAsync();
        }

        public async Task<Inventory?> GetInventoryById(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        public async Task<int> PostInventory(Inventory inventory)
        {
            bool DbEmpty = false;
            int newId;
            if (!await _context.Inventories.AnyAsync(i => i.isdeleted == false)) DbEmpty = true;//de database is leeg
            if (DbEmpty) newId = 1;
            else
            {
                Inventory? latestAddedInv = await _context.Inventories
                .OrderByDescending(i => i.id)
                .FirstOrDefaultAsync();
                newId = latestAddedInv.id + 1;
            }
            if (inventory.item_id == null ||
            inventory.description == null ||
            inventory.item_reference == null ||
            inventory.locations == null ||
            inventory.total_on_hand == null ||
            inventory.total_expected == null ||
            inventory.total_ordered == null ||
            inventory.total_allocated == null ||
            inventory.total_available == null) return 1;//code voor niet alle velden zijn gevuld

            Inventory? doesExist = await _context.Inventories.FindAsync(inventory.id);
            if (doesExist != null) return 2;//Code voor deze inventory bestaat al

            inventory.id = newId;
            inventory.created_at = DateTime.UtcNow;
            inventory.updated_at = DateTime.UtcNow;
            _context.Inventories.Add(inventory);
            int linesChanged = await _context.SaveChangesAsync();
            if (linesChanged != 1) return 3;//Too many lines changed

            return 4;//Succes
        }

        public async Task<int> UpdateInventory(int id, Inventory inventory)
        {
            Inventory? existing = await _context.Inventories.FindAsync(id);

            if (existing == null) return 1;//code voor geen inventory om aan te passen

            if (inventory.item_id == null ||
            inventory.description == null ||
            inventory.item_reference == null ||
            inventory.locations == null ||
            inventory.total_on_hand == null ||
            inventory.total_expected == null ||
            inventory.total_ordered == null ||
            inventory.total_allocated == null ||
            inventory.total_available == null) return 2;//code voor niet alle velden zijn gevuld

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
            return 3;//code voor succes
        }
        public async Task<bool> DeleteInventory(int id)
        {
            Inventory? inventory = await _context.Inventories
                            .Where(i => i.isdeleted == false && i.id == id)
                            .FirstOrDefaultAsync();
            if (inventory == null)
            {
                return false;
            }
            inventory.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}