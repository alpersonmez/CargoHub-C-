using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllLocations(int amount)
        {
            return await _context.Locations.Take(amount).ToListAsync();
        }

        public async Task<Location> GetLocationById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location> AddLocation(Location NewLocation)
        {
            Location location = new Location
            {
                warehouse_id = NewLocation.warehouse_id,
                code = NewLocation.code,
                name = NewLocation.name,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }


        public async Task<bool> UpdateLocation(Location location) 
        {
            Location existingLocation = await _context.Locations.FindAsync(location.id);

            if (existingLocation == null)
            {
                return false;
            }

            existingLocation.warehouse_id = location.warehouse_id;
            existingLocation.code = location.code;
            existingLocation.name = location.name;
            existingLocation.updated_at = DateTime.UtcNow;

            _context.Locations.Update(existingLocation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location?.isdeleted == true || location == null)
            {
                return false;
            }

            location.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}