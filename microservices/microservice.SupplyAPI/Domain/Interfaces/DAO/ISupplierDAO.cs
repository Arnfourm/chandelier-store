using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.DAO
{
    public interface ISupplierDAO
    {
        Task<List<Supplier>> GetSuppliers();
        Task<Supplier> GetSupplierById(Guid id);
        Task<List<Supplier>> GetSupplierByIds(List<Guid> ids);
        Task<Supplier> CreateSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task DeleteSupplierById(Guid id);
    }
}
