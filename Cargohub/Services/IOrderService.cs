using Cargohub.Models;

namespace Cargohub.Services;

public interface IOrderService
{
    public Order GetOrder(int id);
}