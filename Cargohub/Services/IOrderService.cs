using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllOrders();
        public Task<Order> GetOrderById(int id);
        public Task<Order> AddOrder(Order newOrder);
        public Task<bool> UpdateOrder(Order order);
        public Task<bool> DeleteOrder(int id);
    }
}

