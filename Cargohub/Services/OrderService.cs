using Microsoft.EntityFrameworkCore;
using  Cargohub.Models;

namespace Cargohub.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.Take(100).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            Order order = new Order
            {
            Id = newOrder.Id,
            SourceId = newOrder.SourceId,
            OrderDate = newOrder.OrderDate,
            RequestDate = newOrder.RequestDate,
            Reference = newOrder.Reference,
            ReferenceExtra = newOrder.ReferenceExtra,
            OrderStatus = newOrder.OrderStatus,
            Notes = newOrder.Notes,
            ShippingNotes = newOrder.ShippingNotes,
            PickingNotes = newOrder.PickingNotes,
            WarehouseId = newOrder.WarehouseId,
            ShipTo = newOrder.ShipTo,
            BillTo = newOrder.BillTo,
            ShipmentId = newOrder.ShipmentId,
            TotalAmount = newOrder.TotalAmount,
            TotalDiscount = newOrder.TotalDiscount,
            TotalTax = newOrder.TotalTax,
            TotalSurcharge = newOrder.TotalSurcharge,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };


            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            Order existingOrder = await _context.Orders.FindAsync(order.Id);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.Id = order.Id;
            existingOrder.SourceId = order.SourceId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.RequestDate = order.RequestDate;
            existingOrder.Reference = order.Reference;
            existingOrder.ReferenceExtra = order.ReferenceExtra;
            existingOrder.OrderStatus = order.OrderStatus;
            existingOrder.Notes = order.Notes;
            existingOrder.ShippingNotes = order.ShippingNotes;
            existingOrder.PickingNotes = order.PickingNotes;
            existingOrder.WarehouseId = order.WarehouseId;
            existingOrder.ShipTo = order.ShipTo;
            existingOrder.BillTo = order.BillTo;
            existingOrder.ShipmentId = order.ShipmentId;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.TotalDiscount = order.TotalDiscount;
            existingOrder.TotalTax = order.TotalTax;
            existingOrder.TotalSurcharge = order.TotalSurcharge;
            existingOrder.UpdatedAt = DateTime.UtcNow;
        

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
