using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehouses(int amount = 100)
        {
            return await _context.Warehouses.Take(amount).ToListAsync();
        }

        public async Task<Warehouse> GetWarehouseById(int id)
        {
            return await _context.Warehouses.FindAsync(id);
        }

        public async Task<Warehouse> AddWarehouse(Warehouse newWarehouse)
        {
            Warehouse warehouse = new Warehouse
            {
                id = newWarehouse.id,
                code = newWarehouse.code,
                name = newWarehouse.name,
                address = newWarehouse.address,
                zip = newWarehouse.zip,
                city = newWarehouse.city,
                province = newWarehouse.province,
                country = newWarehouse.country,
                contact = newWarehouse.contact,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> UpdateWarehouse(Warehouse warehouse)
        {
            Warehouse existingWarehouse = await _context.Warehouses.FindAsync(warehouse.id);
            if (existingWarehouse == null)
            {
                return false;
            }

            existingWarehouse.code = warehouse.code;
            existingWarehouse.name = warehouse.name;
            existingWarehouse.address = warehouse.address;
            existingWarehouse.zip = warehouse.zip;
            existingWarehouse.province = warehouse.province;
            existingWarehouse.country = warehouse.country;
            existingWarehouse.contact = warehouse.contact;
            existingWarehouse.isdeleted = warehouse.isdeleted;
            existingWarehouse.updated_at = DateTime.UtcNow;


            _context.Warehouses.Update(existingWarehouse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWarehouse(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse?.isdeleted == true || warehouse == null)
            {
                return false;
            }

            warehouse.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}