using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IOrderService
    {
        public List<Order> GetOrders();
        public Order GetOrder(int id);
        //public List<Item> GetItems(int id);
        public bool AddOrder(Order order);
        public bool UpdateOrder(int id, Order order);
        public bool DeleteOrder(int id);


    }
}

