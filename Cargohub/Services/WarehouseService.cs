using Cargohub.Models;

namespace Cargohub.Services;

public class WarehouseService : IWarehouseService
{

    private AppDbContext data;

    public WarehouseService(AppDbContext _data)
    {
        data = _data;
    }

    public List<Warehouse> GetAllWarehouses()
    {
        return data.Warehouses.ToList();
    }

    public Warehouse GetWarehouse(int id)
    {
        return data.Warehouses.FirstOrDefault(warehouse => warehouse.id == id);
    }
    /*
        public Warehouse PostWarehouse()
        {

        }
    */
}