using Cargohub.Models;

namespace Cargohub.Services
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllSuppliers(int amount);
        Task<Supplier> GetSupplierById(int id);
        Task<Supplier> AddSupplier(Supplier supplier);
        Task<bool> UpdateSupplier(Supplier supplier);
        Task<bool> DeleteSupplier(int id);
    }
}
