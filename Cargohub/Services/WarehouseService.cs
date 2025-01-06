using Microsoft.EntityFrameworkCore;
using  Cargohub.Models;

namespace Cargohub.Services
{
    public class WarehouseService : IWarehouseService
    {   
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWareHouses()
        {
            return await _context.Warehouses.Take(100).ToListAsync(); 
        }

        public async Task<Warehouse> GetWareHouseById(int id)
        {
            return await _context.Warehouses.FindAsync(id);
        }

        public async Task<Warehouse> AddWarehouse(Warehouse newWarehouse)
        {
             // Validatie logica
            if (newWarehouse.gevarenclassificatie.HasValue)
            {
                if (newWarehouse.gevarenclassificatie < 1 || newWarehouse.gevarenclassificatie > 5)
                {
                    throw new ArgumentException("Gevarenclassificatie moet tussen 1 en 5 liggen of null zijn.");
                }
            }

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
                gevarenclassificatie = newWarehouse.gevarenclassificatie, // Nullable eigenschap
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }


        public async Task<bool> DeleteWarehouse(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return false;
            }
            
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}