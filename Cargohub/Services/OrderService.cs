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
                id = o.id,
                source_id = o.source_id,
                order_date = o.order_date,
                request_date = o.request_date,
                reference = o.reference,
                reference_extra = o.reference_extra,
                order_status = o.order_status,
                notes = o.notes,
                shipping_notes = o.shipping_notes,
                picking_notes = o.picking_notes,
                warehouse_id = o.warehouse_id,
                ship_to = o.ship_to,
                bill_to = o.bill_to,
                total_amount = o.total_amount,
                total_discount = o.total_discount,
                total_tax = o.total_tax,
                total_surcharge = o.total_surcharge,
                created_at = o.created_at,
                updated_at = o.updated_at,
                isdeleted = o.isdeleted,
                items = o.Items.Select(i => new OrderStockDto
                {
                    ItemId = i.ItemId,
                    Amount = i.amount
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto?> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items) // Include related stocks
                .FirstOrDefaultAsync(o => o.id == id);

            if (order == null)
                return null;

            // Map entity to DTO
            return new OrderDto
            {
                id = order.id,
                source_id = order.source_id,
                order_date = order.order_date,
                request_date = order.request_date,
                reference = order.reference,
                reference_extra = order.reference_extra,
                order_status = order.order_status,
                notes = order.notes,
                shipping_notes = order.shipping_notes,
                picking_notes = order.picking_notes,
                warehouse_id = order.warehouse_id,
                ship_to = order.ship_to,
                bill_to = order.bill_to,
                total_amount = order.total_amount,
                total_discount = order.total_discount,
                total_tax = order.total_tax,
                total_surcharge = order.total_surcharge,
                created_at = order.created_at,
                updated_at = order.updated_at,
                isdeleted = order.isdeleted,
                items = order.Items.Select(i => new OrderStockDto
                {
                    ItemId = i.ItemId,
                    Amount = i.amount
                }).ToList()
            };
        }

        public async Task<OrderDto> AddOrder(OrderDto newOrderDto)
        {
            // Map DTO to Entity
            var order = new Order
            {
                source_id = newOrderDto.source_id,
                order_date = newOrderDto.order_date,
                request_date = newOrderDto.request_date,
                reference = newOrderDto.reference,
                reference_extra = newOrderDto.reference_extra,
                order_status = newOrderDto.order_status,
                notes = newOrderDto.notes,
                shipping_notes = newOrderDto.shipping_notes,
                picking_notes = newOrderDto.picking_notes,
                warehouse_id = newOrderDto.warehouse_id,
                ship_to = newOrderDto.ship_to,
                bill_to = newOrderDto.bill_to,
                total_amount = newOrderDto.total_amount,
                total_discount = newOrderDto.total_discount,
                total_tax = newOrderDto.total_tax,
                total_surcharge = newOrderDto.total_surcharge,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                isdeleted = newOrderDto.isdeleted,
                Items = newOrderDto.items.Select(i => new OrderStock
                {
                    ItemId = i.ItemId,
                    amount = i.Amount
                }).ToList()
            };

            // Add to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Map back to DTO to return
            return new OrderDto
            {
                id = order.id,
                source_id = order.source_id,
                order_date = order.order_date,
                request_date = order.request_date,
                reference = order.reference,
                reference_extra = order.reference_extra,
                order_status = order.order_status,
                notes = order.notes,
                shipping_notes = order.shipping_notes,
                picking_notes = order.picking_notes,
                warehouse_id = order.warehouse_id,
                ship_to = order.ship_to,
                bill_to = order.bill_to,
                total_amount = order.total_amount,
                total_discount = order.total_discount,
                total_tax = order.total_tax,
                total_surcharge = order.total_surcharge,
                created_at = order.created_at,
                updated_at = order.updated_at,
                isdeleted = order.isdeleted,
                items = order.Items.Select(i => new OrderStockDto
                {
                    ItemId = i.ItemId,
                    Amount = i.amount
                }).ToList()
            };
        }



        public async Task<bool> UpdateOrder(OrderDto updatedOrderDto)
        {
            // Fetch the existing order
            var existingOrder = await _context.Orders
                .Include(o => o.Items) // Include related stocks
                .FirstOrDefaultAsync(o => o.id == updatedOrderDto.id);

            if (existingOrder == null)
            {
                return false;
            }

            // Update properties
            existingOrder.source_id = updatedOrderDto.source_id;
            existingOrder.order_date = updatedOrderDto.order_date;
            existingOrder.request_date = updatedOrderDto.request_date;
            existingOrder.reference = updatedOrderDto.reference;
            existingOrder.reference_extra = updatedOrderDto.reference_extra;
            existingOrder.order_status = updatedOrderDto.order_status;
            existingOrder.notes = updatedOrderDto.notes;
            existingOrder.shipping_notes = updatedOrderDto.shipping_notes;
            existingOrder.picking_notes = updatedOrderDto.picking_notes;
            existingOrder.warehouse_id = updatedOrderDto.warehouse_id;
            existingOrder.ship_to = updatedOrderDto.ship_to;
            existingOrder.bill_to = updatedOrderDto.bill_to;
            existingOrder.total_amount = updatedOrderDto.total_amount;
            existingOrder.total_discount = updatedOrderDto.total_discount;
            existingOrder.total_tax = updatedOrderDto.total_tax;
            existingOrder.total_surcharge = updatedOrderDto.total_surcharge;
            existingOrder.updated_at = DateTime.UtcNow;

            // Handle related items
            existingOrder.Items = updatedOrderDto.items.Select(i => new OrderStock
            {
                ItemId = i.ItemId,
                amount = i.Amount
            }).ToList();

            // Save changes to the database
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
