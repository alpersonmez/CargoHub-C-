using Cargohub.Models;

namespace Cargohub.Services
{
    public class OrderService : IOrderService
    {

        private AppDbContext data;

        public OrderService(AppDbContext _data)
        {
            data = _data;
        }

        public List<Order> GetOrders()
        {
            if (data.Orders.Count() == 0) return new List<Order>();
            return data.Orders.ToList();
        }

        public Order? GetOrder(int id)
        {
            //if(data.Orders.SingleOrDefault(x => x.id == id) == null) return new Order()
            return data.Orders.SingleOrDefault(x => x.id == id);
        }

        // public List<Item>? GetItems(int id)
        // {
        //     if (data.Orders.SingleOrDefault(x => x.id == id) is null) return null;
        //     return data.Orders.Where(x => x.id == id).Single().Items.ToList();
        // }
        public bool AddOrder(Order order)
        {
            if (order is null || data.Orders.Contains(order)) return false;
            data.Orders.Add(order);
            data.SaveChanges();
            return true;
        }
        public bool UpdateOrder(int id, Order order)
        {
            if (order is null || id != order.id) return false;
            if (data.Orders.SingleOrDefault(x => x.id == id) is null) return false;
            data.Orders.Remove(data.Orders.Where(x => x.id == id).Single());
            data.Orders.Add(order);
            data.SaveChanges();
            return true;
        }
        public bool DeleteOrder(int id)
        {
            if (data.Orders.SingleOrDefault(x => x.id == id) is null) return false;
            data.Orders.Remove(data.Orders.Where(x => x.id == id).Single());
            data.SaveChanges();
            return true;
        }
    }
}

