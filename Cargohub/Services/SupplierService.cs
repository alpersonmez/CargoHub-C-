using Microsoft.EntityFrameworkCore;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllSuppliers(int amount = 100)
        {
            return await _context.Supplier.Take(amount).ToListAsync();
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            return await _context.Supplier.FindAsync(id);
        }

        public async Task<Supplier> AddSupplier(Supplier Newsupplier)
        {
            Supplier supplier = new Supplier
            {
                code = Newsupplier.code,
                name = Newsupplier.name,
                address = Newsupplier.address,
                address_extra = Newsupplier.address_extra,
                city = Newsupplier.city,
                zip_code = Newsupplier.zip_code,
                province = Newsupplier.province,
                country = Newsupplier.country,
                contact_name = Newsupplier.contact_name,
                phone_number = Newsupplier.phone_number,
                reference = Newsupplier.reference,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Supplier.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }


        public async Task<bool> UpdateSupplier(Supplier supplier)
        {
            Supplier ExistingSupplier = await _context.Supplier.FindAsync(supplier.id);

            if (ExistingSupplier == null)
            {
                return false;
            }

            ExistingSupplier.code = supplier.code;
            ExistingSupplier.name = supplier.name;
            ExistingSupplier.updated_at = DateTime.UtcNow;

            _context.Supplier.Update(ExistingSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier?.isdeleted == true || supplier == null)
            {
                return false;
            }

            supplier.isdeleted = true;
            //_context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}