using Cargohub.Models;

namespace Cargohub.Services
{
    public class ShipmentService : IShipmentService
    {

        private AppDbContext data;

        public ShipmentService(AppDbContext _data)
        {
            data = _data;
        }

        public List<Shipment> GetShipments()
        {
            if (data.Shipments.Count() == 0) return new List<Shipment>();
            return data.Shipments.ToList();
        }

        public Shipment? GetShipment(int id)
        {
            //if(data.Shipment.SingleOrDefault(x => x.id == id) == null) return new Shipment()
            return data.Shipments.SingleOrDefault(x => x.id == id);
        }

        // public List<Item>? GetItems(int id)
        // {
        //     if (data.Shipment.SingleOrDefault(x => x.id == id) is null) return null;
        //     return data.Shipment.Where(x => x.id == id).Single().Items.ToList();
        // }
        public bool AddShipment(Shipment shipment)
        {
            if (shipment is null || data.Shipments.Contains(shipment)) return false;
            data.Shipments.Add(shipment);
            data.SaveChanges();
            return true;
        }
        public bool UpdateShipment(int id, Shipment shipment)
        {
            if (shipment is null || id != shipment.id) return false;
            if (data.Shipments.SingleOrDefault(x => x.id == id) is null) return false;
            data.Shipments.Remove(data.Shipments.Where(x => x.id == id).Single());
            data.Shipments.Add(shipment);
            data.SaveChanges();
            return true;
        }
        public bool DeleteShipment(int id)
        {
            if (data.Shipments.SingleOrDefault(x => x.id == id) is null) return false;
            data.Shipments.Remove(data.Shipments.Where(x => x.id == id).Single());
            data.SaveChanges();
            return true;
        }
    }
}

