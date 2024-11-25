using Cargohub.Models;
using System.Text.Json;

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
        return data.Warehouses.Where(w => w.id <= 100).ToList();
    }

    public Warehouse? GetWarehouse(int id)
    {
        return data.Warehouses.FirstOrDefault(warehouse => warehouse.id == id);
    }

    public Warehouse? AddWarehouse(Warehouse warehouseToAdd)
    {
        // Check if a warehouse with the same ID already exists
        Warehouse? alreadyExists = data.Warehouses.FirstOrDefault(w => w.id == warehouseToAdd.id);
        if (alreadyExists != null) return null;

        // Add the warehouse to the database
        data.Warehouses.Add(warehouseToAdd);
        data.SaveChanges();
        return warehouseToAdd;

    }
}
