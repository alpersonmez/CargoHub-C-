using Microsoft.EntityFrameworkCore;
using Cargohub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Services
{
    public class ItemLinesService : IItemLinesService
    {
        private readonly AppDbContext _context;

        public ItemLinesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemLines>> GetAllItemLines()
        {
            return await _context.Item_lines.Take(100).ToListAsync();  // Take(100) as a limit
        }

        public async Task<ItemLines> GetItemLineById(int id)
        {
            return await _context.Item_lines.FindAsync(id);
        }

        public async Task<ItemLines> UpdateItemLine(int id, ItemLines updatedItemLine)
        {
            var existingItemLine = await _context.Item_lines.FindAsync(id);
            if (existingItemLine == null) return null;

            existingItemLine.name = updatedItemLine.name;
            existingItemLine.description = updatedItemLine.description;
            existingItemLine.updated_at = DateTime.UtcNow;

            _context.Item_lines.Update(existingItemLine);
            await _context.SaveChangesAsync();
            return existingItemLine;
        }

        public async Task<bool> DeleteItemLine(int id)
        {
            var itemLine = await _context.Item_lines.FindAsync(id);
            if (itemLine == null) return false;

            _context.Item_lines.Remove(itemLine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
