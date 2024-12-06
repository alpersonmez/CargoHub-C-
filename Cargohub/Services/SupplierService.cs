using Microsoft.EntityFrameworkCore;
using  Cargohub.Models;

namespace Cargohub.Services
{
    public class SupplierService : ISupplierService
    {   
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllSupplier()
        {
            return await _context.Supplier.Take(100).ToListAsync(); 
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            return await _context.Supplier.FindAsync(id);
        }

        public async Task<Supplier> AddSupplier(Supplier Newsupplier)
        {
            Supplier supplier = new Supplier
            {
                Code = Newsupplier.Code,
                Name = Newsupplier.Name,
                Address = Newsupplier.Address,
                AddressExtra = Newsupplier.AddressExtra,
                City = Newsupplier.City,
                ZipCode = Newsupplier.ZipCode,
                Country = Newsupplier.Country,
                ContactName = Newsupplier.ContactName,
                PhoneNumber = Newsupplier.PhoneNumber,
                Reference = Newsupplier.Reference,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Supplier.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }


        public async Task<bool> UpdateSupplier(Supplier supplier) 
        {
            Supplier ExistingSupplier = await _context.Supplier.FindAsync(supplier.Id);
            
            if (ExistingSupplier == null)
            {
                return false;
            }

            ExistingSupplier.Code = supplier.Code;
            ExistingSupplier.Name = supplier.Name;
            ExistingSupplier.UpdatedAt = DateTime.UtcNow;

            _context.Supplier.Update(ExistingSupplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return false;
            }
            
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}