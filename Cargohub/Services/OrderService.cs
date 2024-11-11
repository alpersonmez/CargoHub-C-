using Cargohub.Models;

namespace Cargohub.Services;

public class OrderService : IOrderService
{

    private AppDbContext data;

    public OrderService(AppDbContext _data)
    {
        data = _data;
    }

    public Order GetOrder(int id)
    {
        return null; //Replace with database logic
    }
}