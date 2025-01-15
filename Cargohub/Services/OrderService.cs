using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetAllOrders(int amount = 100)
        {
            return await _context.Orders.Take(amount).ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            Order order = new Order
            {
                id = newOrder.id,
                source_id = newOrder.source_id,
                order_date = newOrder.order_date,
                request_date = newOrder.request_date,
                reference = newOrder.reference,
                reference_extra = newOrder.reference_extra,
                order_status = newOrder.order_status,
                notes = newOrder.notes,
                shipping_notes = newOrder.shipping_notes,
                picking_notes = newOrder.picking_notes,
                warehouse_id = newOrder.warehouse_id,
                ship_to = newOrder.ship_to,
                bill_to = newOrder.bill_to,
                shipment_id = newOrder.shipment_id,
                total_amount = newOrder.total_amount,
                total_discount = newOrder.total_discount,
                total_tax = newOrder.total_tax,
                total_surcharge = newOrder.total_surcharge,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };


            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            Order existingOrder = await _context.Orders.FindAsync(order.id);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.id = order.id;
            existingOrder.source_id = order.source_id;
            existingOrder.order_date = order.order_date;
            existingOrder.request_date = order.request_date;
            existingOrder.reference = order.reference;
            existingOrder.reference_extra = order.reference_extra;
            existingOrder.order_status = order.order_status;
            existingOrder.notes = order.notes;
            existingOrder.shipping_notes = order.shipping_notes;
            existingOrder.picking_notes = order.picking_notes;
            existingOrder.warehouse_id = order.warehouse_id;
            existingOrder.ship_to = order.ship_to;
            existingOrder.bill_to = order.bill_to;
            existingOrder.shipment_id = order.shipment_id;
            existingOrder.total_amount = order.total_amount;
            existingOrder.total_discount = order.total_discount;
            existingOrder.total_tax = order.total_tax;
            existingOrder.total_surcharge = order.total_surcharge;
            existingOrder.created_at = DateTime.UtcNow;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order?.isdeleted == true || order == null)
            {
                return false;
            }

            order.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
