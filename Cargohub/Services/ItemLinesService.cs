using Microsoft.EntityFrameworkCore;
using Cargohub.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<ItemLines>> GetAllItemLines(int amount)
        {
            return await _context.Item_lines.Take(amount).ToListAsync();
        }

        public async Task<ItemLines> GetItemLineById(int id)
        {
            return await _context.Item_lines.FindAsync(id);
        }

        public async Task<bool> UpdateItemLine(ItemLines itemline)
        {
            ItemLines existingItemLine = await _context.Item_lines.FindAsync(itemline.id);

            if (existingItemLine == null)
            {
                return false;
            }

            existingItemLine.name = itemline.name;
            existingItemLine.description = itemline.description;
            existingItemLine.updated_at = DateTime.UtcNow;

            _context.Item_lines.Update(existingItemLine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemLine(int id)
        {
            var itemLine = await _context.Item_lines.FindAsync(id);
            if (itemLine?.isdeleted == true || itemLine == null)
            {
                return false;
            }

            itemLine.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}