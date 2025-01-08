using Cargohub.Models;

namespace Cargohub.Services
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllLocations(int amount = 100);
        Task<Location> GetLocationById(int id);
        Task<Location> AddLocation(Location location);
        Task<bool> UpdateLocation(Location location);
        Task<bool> DeleteLocation(int id);
    }
}
