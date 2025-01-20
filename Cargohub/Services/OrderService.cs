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

        public async Task<List<OrderDto>> GetAllOrders(int amount)
        {
            var orders = await _context.Orders
                .Include(o => o.Items) // Include related stocks
                .Take(amount)
                .ToListAsync();

            // Map entities to DTOs
            return orders.Select(o => new OrderDto
            {
                Id = o.id,
                SourceId = o.source_id,
                OrderDate = o.order_date,
                RequestDate = o.request_date,
                Reference = o.reference,
                OrderStatus = o.order_status,
                TotalAmount = o.total_amount,
                Items = o.Items.Select(i => new OrderStockDto
                {
                    ItemId = i.ItemId,
                    Amount = i.amount
                }).ToList()
            }).ToList();
        }


        public async Task<OrderDto?> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.id == id);

            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.id,
                SourceId = order.source_id,
                OrderDate = order.order_date,
                RequestDate = order.request_date,
                Reference = order.reference,
                OrderStatus = order.order_status,
                TotalAmount = order.total_amount,
                Items = order.Items.Select(i => new OrderStockDto
                {
                    ItemId = i.ItemId,
                    Amount = i.amount
                }).ToList()
            };
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
            var existingOrder = await _context.Orders.FindAsync(order.id);
            if (existingOrder == null)
            {
                return false;
            }

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
            existingOrder.total_amount = order.total_amount;
            existingOrder.total_discount = order.total_discount;
            existingOrder.total_tax = order.total_tax;
            existingOrder.total_surcharge = order.total_surcharge;
            existingOrder.updated_at = DateTime.UtcNow;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || order.isdeleted)
            {
                return false;
            }

            order.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisconnectShipmentsFromOrder(int orderId, List<int> shipmentIds)
        {
            // Validate the order exists
            var order = await _context.Orders
                .Include(o => o.OrderShipments)
                .FirstOrDefaultAsync(o => o.id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Remove the specific links
            var linksToRemove = order.OrderShipments
                .Where(os => shipmentIds.Contains(os.shipment_id))
                .ToList();

            if (!linksToRemove.Any())
            {
                throw new Exception("No matching shipments found for the order.");
            }

            _context.OrderShipments.RemoveRange(linksToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
