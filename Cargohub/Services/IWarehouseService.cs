using Cargohub.Models;

namespace Cargohub.Services;

public interface IWarehouseService
{
    public Warehouse GetWarehouse(int id);
    public List<Warehouse> GetAllWarehouses();
}