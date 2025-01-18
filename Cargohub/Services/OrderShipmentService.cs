using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargohub.Models;


public class OrderShipmentService : IOrderShipmentService
{
    private readonly AppDbContext _context;

    public OrderShipmentService(AppDbContext context)
    {
        _context = context;
    }

    // Link multiple orders to a single shipment
    public async Task<bool> LinkOrdersToShipment(int shipmentId, List<int> orderIds, int quantity = 0)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.id == shipmentId);
        if (shipment == null)
        {
            throw new Exception("Shipment not found.");
        }

        var orders = await _context.Orders.Where(o => orderIds.Contains(o.id)).ToListAsync();
        if (orders.Count != orderIds.Count)
        {
            throw new Exception("Some orders were not found.");
        }

        foreach (var order in orders)
        {
            var orderShipment = new OrderShipment
            {
                order_id = order.id,
                shipment_id = shipmentId,
                quantity = quantity
            };
            _context.OrderShipments.Add(orderShipment);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // Link multiple shipments to a single order
    public async Task<bool> LinkShipmentsToOrder(int orderId, List<int> shipmentIds, int quantity = 0)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.id == orderId);
        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        var shipments = await _context.Shipments.Where(s => shipmentIds.Contains(s.id)).ToListAsync();
        if (shipments.Count != shipmentIds.Count)
        {
            throw new Exception("Some shipments were not found.");
        }

        foreach (var shipment in shipments)
        {
            var orderShipment = new OrderShipment
            {
                order_id = orderId,
                shipment_id = shipment.id,
                quantity = quantity
            };
            _context.OrderShipments.Add(orderShipment);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // Get all shipments linked to an order
    public async Task<List<Shipment>> GetShipmentsForOrder(int orderId)
    {
        return await _context.OrderShipments
            .Where(os => os.order_id == orderId)
            .Select(os => os.Shipment)
            .ToListAsync();
    }

    // Get all orders linked to a shipment
    public async Task<List<Order>> GetOrdersForShipment(int shipmentId)
    {
        return await _context.OrderShipments
            .Where(os => os.shipment_id == shipmentId)
            .Select(os => os.Order)
            .ToListAsync();
    }
}
