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

        public async Task<List<Shipment>> GetAllShipments(int amount)
        {
            return await _context.Shipments
                .Include(s => s.OrderShipments)
                .ThenInclude(os => os.Order)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<Shipment> GetShipmentById(int id)
        {
            return await _context.Shipments
                .Include(s => s.OrderShipments)
                .ThenInclude(os => os.Order)
                .FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task<Shipment> AddShipment(Shipment newShipment)
        {
            newShipment.created_at = DateTime.UtcNow;
            newShipment.updated_at = DateTime.UtcNow;

            _context.Shipments.Add(newShipment);
            await _context.SaveChangesAsync();
            return newShipment;
        }

        public async Task<bool> UpdateShipment(Shipment shipment)
        {
            var existingShipment = await _context.Shipments.FindAsync(shipment.id);
            if (existingShipment == null)
            {
                return false;
            }

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
            if (shipment == null || shipment.isdeleted)
            {
                return false;
            }

            shipment.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DisconnectOrdersFromShipment(int shipmentId, List<int> orderIds)
        {
            // Validate the shipment exists
            var shipment = await _context.Shipments
                .Include(s => s.OrderShipments)
                .FirstOrDefaultAsync(s => s.id == shipmentId);

            if (shipment == null)
            {
                throw new Exception("Shipment not found.");
            }

            // Remove the specific links
            var linksToRemove = shipment.OrderShipments
                .Where(os => orderIds.Contains(os.order_id))
                .ToList();

            if (!linksToRemove.Any())
            {
                throw new Exception("No matching orders found for the shipment.");
            }

            _context.OrderShipments.RemoveRange(linksToRemove);
            await _context.SaveChangesAsync();
            return true;
        }



        // public async Task<int> GetTotalWeight()
        // {
        //     var shipments = await _context.Shipments.ToListAsync();
        //     foreach (Shipment shipment in shipments)
        //     {
        //         foreach (OrderShipment orderShipment in shipment.OrderShipments){
        //             orderShipment.Order.
        //         }
        //     }
        //     return 1;
        // }


    }
}


