using Cargohub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDockService
{
    Task<List<Dock>> GetAllDocks(int amount);
    Task<Dock?> GetDockById(int id);
    Task<List<Dock>> GetDocksByWarehouseId(int warehouseId);
    Task<Dock> AddDockAsync(Dock newDock);
    Task<bool> UpdateDockAsync(int id, Dock updatedDock);
    Task<bool> RemoveDockAsync(int id);
}
