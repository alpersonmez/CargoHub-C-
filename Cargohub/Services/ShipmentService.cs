using Cargohub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly AppDbContext _context;

        public ShipmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shipment>> GetAllShipments()
        {
            return await _context.Shipments.Take(100).ToListAsync();
        }

        public async Task<Shipment> GetShipmentById(int id)
        {
            return await _context.Shipments.FindAsync(id);
        }

        public async Task<Shipment> AddShipment(Shipment newShipment)
        {
            Shipment shipment = new Shipment
            {
            OrderId = newShipment.OrderId,
            SourceId = newShipment.SourceId, 
            OrderDate = newShipment.OrderDate,
            RequestDate = newShipment.RequestDate,
            ShipmentDate = newShipment.ShipmentDate,
            ShipmentType = newShipment.ShipmentType,
            ShipmentStatus = newShipment.ShipmentStatus,
            Notes = newShipment.Notes,
            CarrierCode = newShipment.CarrierCode,
            CarrierDescription = newShipment.CarrierDescription,
            ServiceCode = newShipment.ServiceCode,
            PaymentType = newShipment.PaymentType,
            TransferMode = newShipment.TransferMode,
            TotalPackageCount = newShipment.TotalPackageCount,
            TotalPackageWeight = newShipment.TotalPackageWeight,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow};

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        public async Task<bool> UpdateShipment(Shipment shipment)
        {
            Shipment existingShipment = await _context.Shipments.FindAsync(shipment.Id);
            if (existingShipment == null)
            {
                return false;
            }

            existingShipment.OrderId = shipment.OrderId;
            existingShipment.SourceId = shipment.SourceId;
            existingShipment.OrderDate = shipment.OrderDate;
            existingShipment.RequestDate = shipment.RequestDate;
            existingShipment.ShipmentDate = shipment.ShipmentDate;
            existingShipment.ShipmentType = shipment.ShipmentType;
            existingShipment.ShipmentStatus = shipment.ShipmentStatus;
            existingShipment.Notes = shipment.Notes;
            existingShipment.CarrierCode = shipment.CarrierCode;
            existingShipment.CarrierDescription = shipment.CarrierDescription;
            existingShipment.ServiceCode = shipment.ServiceCode;
            existingShipment.PaymentType = shipment.PaymentType;
            existingShipment.TransferMode = shipment.TransferMode;
            existingShipment.TotalPackageCount = shipment.TotalPackageCount;
            existingShipment.TotalPackageWeight = shipment.TotalPackageWeight;
            existingShipment.UpdatedAt = DateTime.UtcNow;

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
