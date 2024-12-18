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

        public List<Item_lines> GetAllItem_lines()
        {
            return _context.Item_lines.Take(100).ToList(); // Take(100) is that the limit is 100 locations
        }

        public Item_lines GetItem_linesById(int id)
        {
            return _context.Item_lines.FirstOrDefault(Item_lines => Item_lines.id == id);
        }

        public Item_lines UpdateItem_lines(int id, Item_lines Updateditem_lines)
        {
            var existingItemLine = _context.Item_lines.SingleOrDefault(Item_lines => Item_lines.id == id);
            if (existingItemLine == null) return null;

            existingItemLine.name = Updateditem_lines.name;
            existingItemLine.description = Updateditem_lines.description;
            existingItemLine.update_at = DateTime.UtcNow;

            _context.SaveChanges();
            return existingItemLine;
        }

        public bool DeleteItem_lines(int id)
        {
            var itemLine = GetItem_linesById(id);
            if (itemLine == null) return false;

            _context.Item_lines.Remove(itemLine);
            _context.SaveChanges();
            return true;
        }
    }
}