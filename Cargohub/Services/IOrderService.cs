using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrders(int amount);
        Task<OrderDto> GetOrderById(int id);
        Task<OrderDto> AddOrder(OrderDto newOrder);
        Task<bool> UpdateOrder(OrderDto order);
        public Task<bool> DeleteOrder(int id);
        public Task<bool> DisconnectShipmentsFromOrder(int orderId, List<int> shipmentIds);
    }
}

