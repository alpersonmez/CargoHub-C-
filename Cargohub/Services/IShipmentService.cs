using Cargohub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargohub.Services
{
    public interface IShipmentService
    {
        Task<List<Shipment>> GetAllShipments(int amount = 100);
        Task<Shipment> GetShipmentById(int id);
        Task<Shipment> AddShipment(Shipment shipment);
        Task<bool> UpdateShipment(Shipment shipment);
        Task<bool> DeleteShipment(int id);
        Task<bool> AddOrdersToShipment(int shipmentId, List<int> orderIds);
        Task<bool> ReleaseOrderFromShipment(int orderId);
    }
}
