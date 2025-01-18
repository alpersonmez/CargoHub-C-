using Cargohub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderShipmentService
{
    Task<bool> LinkOrdersToShipment(int shipmentId, List<int> orderIds, int quantity = 0);
    Task<bool> LinkShipmentsToOrder(int orderId, List<int> shipmentIds, int quantity = 0);
    Task<List<Shipment>> GetShipmentsForOrder(int orderId);
    Task<List<Order>> GetOrdersForShipment(int shipmentId);
}
