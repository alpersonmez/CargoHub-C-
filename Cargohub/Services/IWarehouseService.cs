using Cargohub.Models;

namespace Cargohub.Services;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetAllWareHouses();
    Task<Warehouse> GetWareHouseById(int id);
    Task<Warehouse> AddWarehouse(Warehouse warehouse);
    //Task<bool> UpdateWareHouse(Warehouse warehouse);
    //moet nog ff kijken hoe ik dit ga doen
    Task<bool> DeleteWarehouse(int id);
}