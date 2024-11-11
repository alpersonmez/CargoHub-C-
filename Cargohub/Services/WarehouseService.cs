using Cargohub.Models;

namespace Cargohub.Services;

public class WarehouseService : IWarehouseService
{

    private AppDbContext data;

    public WarehouseService(AppDbContext _data)
    {
        data = _data;
    }

    public Warehouse GetWarehouse(int id)
    {
        return null;
    }
}