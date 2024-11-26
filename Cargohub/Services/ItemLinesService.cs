using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using  Cargohub.Models;

namespace Cargohub.Services{

    public class ItemlinesService : IitemlinesService
    {
        private readonly AppDbContext _context;

        public ItemlinesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item_lines>> GetAllItem_lines()
        {
            return await _context.Item_lines.Take(100).ToListAsync(); // Take(100) is that the limit is 100 locations
        }

        public async Task<Item_lines> GetItem_linesById(int id)
        {
            return await _context.Item_lines.FindAsync(id);
        }

        public async Task<bool> UpdateItem_lines(Item_lines item_Lines)
        {
            Item_lines existing = await _context.Item_lines.FindAsync(item_Lines.id);
            
            if (existing == null)
            {
                return false;
            }

            existing.name = item_Lines.name;
            existing.description = item_Lines.description;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Item_lines.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItem_lines(int id)
        {
            var item = await _context.Item_lines.FindAsync(id);
            if (item == null)
            {
                return false;
            }
            
            _context.Item_lines.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}