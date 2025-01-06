using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly AppDbContext _context;

        public ShipmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shipment>> GetAllShipments(int amount = 100)
        {
            return await _context.Shipments.Take(amount).ToListAsync();
        }

        public async Task<Shipment> GetShipmentById(int id)
        {
            return await _context.Shipments.FindAsync(id);
        }

        public async Task<Shipment> AddShipment(Shipment newShipment)
        {
            Shipment shipment = new Shipment
            {
                order_id = newShipment.order_id,
                source_id = newShipment.source_id,
                order_date = newShipment.order_date,
                request_date = newShipment.request_date,
                shipment_date = newShipment.shipment_date,
                shipment_type = newShipment.shipment_type,
                shipment_status = newShipment.shipment_status,
                notes = newShipment.notes,
                carrier_code = newShipment.carrier_code,
                carrier_description = newShipment.carrier_description,
                service_code = newShipment.service_code,
                payment_type = newShipment.payment_type,
                transfer_mode = newShipment.transfer_mode,
                total_package_count = newShipment.total_package_count,
                total_package_weight = newShipment.total_package_weight,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };


            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        public async Task<bool> UpdateShipment(Shipment shipment)
        {
            Shipment existingShipment = await _context.Shipments.FindAsync(shipment.id);
            if (existingShipment == null)
            {
                return false;
            }

            existingShipment.order_id = shipment.order_id;
            existingShipment.source_id = shipment.source_id;
            existingShipment.order_date = shipment.order_date;
            existingShipment.request_date = shipment.request_date;
            existingShipment.shipment_date = shipment.shipment_date;
            existingShipment.shipment_type = shipment.shipment_type;
            existingShipment.shipment_status = shipment.shipment_status;
            existingShipment.notes = shipment.notes;
            existingShipment.carrier_code = shipment.carrier_code;
            existingShipment.carrier_description = shipment.carrier_description;
            existingShipment.service_code = shipment.service_code;
            existingShipment.payment_type = shipment.payment_type;
            existingShipment.transfer_mode = shipment.transfer_mode;
            existingShipment.total_package_count = shipment.total_package_count;
            existingShipment.total_package_weight = shipment.total_package_weight;
            existingShipment.updated_at = DateTime.UtcNow;


            _context.Shipments.Update(existingShipment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShipment(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return false;
            }

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
