using Microsoft.EntityFrameworkCore;
using Cargohub.Models;
using System.Text.RegularExpressions;

namespace Cargohub.Services
{
    public class DockService : IDockService
    {
        private readonly AppDbContext _context;

        public DockService(AppDbContext context)
        {
            _context = context;
        }

        // Get all docks with a limit
        public async Task<List<Dock>> GetAllDocks(int amount)
        {
            return await _context.Docks
                .Include(d => d.warehouse) // Include related warehouse data
                .Take(amount) // Limit the number of results
                .ToListAsync();
        }

        // Get a dock by ID
        public async Task<Dock?> GetDockById(int id)
        {
            return await _context.Docks
                .Include(d => d.warehouse) // Include related warehouse data
                .FirstOrDefaultAsync(d => d.id == id && !d.is_deleted);
        }

        // Get docks by Warehouse ID
        public async Task<List<Dock>> GetDocksByWarehouseId(int warehouseId)
        {
            return await _context.Docks
                .Where(d => d.warehouse_id == warehouseId && !d.is_deleted)
                .Include(d => d.warehouse)
                .ToListAsync();
        }


        public async Task<Dock> AddDockAsync(Dock newDock)
        {
            // Get the latest dock based on the code
            var lastDock = await _context.Docks
                .OrderByDescending(d => d.id) // Order by primary key (or you could order by code if needed)
                .FirstOrDefaultAsync();

            if (lastDock != null)
            {
                var match = Regex.Match(lastDock.code, @"\d+");
                int lastNumber = match.Success ? int.Parse(match.Value) : 0;

                newDock.code = $"DCK{(lastNumber + 1):D6}";
            }
            else
            {
                newDock.code = "DCK000001";
            }

            // Set other default fields
            newDock.created_at = DateTime.UtcNow;
            newDock.updated_at = DateTime.UtcNow;

            // Add to the database
            _context.Docks.Add(newDock);
            await _context.SaveChangesAsync();

            return newDock;
        }


        public async Task<bool> UpdateDockAsync(int id, Dock updatedDock)
        {
            var existingDock = await _context.Docks.FindAsync(id);

            if (existingDock == null)
            {
                return false;
            }

            // Validation: Prevent modification of the code field
            if (existingDock.code != updatedDock.code && !string.IsNullOrEmpty(updatedDock.code))
            {
                throw new InvalidOperationException("Code cannot be modified.");
            }

            // Retain the original code value
            updatedDock.code = existingDock.code;

            // Update other fields
            existingDock.status = updatedDock.status;
            existingDock.description = updatedDock.description;
            existingDock.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }



        // Soft delete a dock
        public async Task<bool> RemoveDockAsync(int id)
        {
            var dock = await _context.Docks.FindAsync(id);
            if (dock == null || dock.is_deleted)
            {
                return false;
            }

            dock.is_deleted = true;
            dock.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
