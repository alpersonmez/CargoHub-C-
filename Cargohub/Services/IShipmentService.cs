using Cargohub.Models;

namespace Cargohub.Services
{
    public interface IShipmentService
    {
        public List<Shipment> GetShipments();
        public Shipment GetShipment(int id);
        //public List<Order> GetOrders(int id);
        //public List<Item> GetItems(int id);
        public bool AddShipment(Shipment shipment);
        public bool UpdateShipment(int id, Shipment shipment);
        public bool DeleteShipment(int id);


    }
}

