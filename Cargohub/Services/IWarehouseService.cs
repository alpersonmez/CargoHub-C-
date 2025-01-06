using Cargohub.Models;

namespace Cargohub.Services;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetAllWarehouses(int amount = 100);
    Task<Warehouse> GetWarehouseById(int id);
    Task<Warehouse> AddWarehouse(Warehouse warehouse);
    Task<bool> UpdateWarehouse(Warehouse warehouse);
    Task<bool> DeleteWarehouse(int id);
}