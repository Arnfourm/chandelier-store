using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierResponse>> GetAllSuppliers();
        Task<Supplier> GetSingleSupplierById(Guid id);
        Task<SupplierResponse> GetSingleSupplierResponseByIdAsync(Guid id);
        Task<IEnumerable<SupplierResponse>> GetListSupplierResponseByIds(List<Guid> ids);
        Task<SupplierResponse> CreateNewSupplier(SupplierRequest request);
        Task UpdateSingleSupplier(Guid id, SupplierRequest request);
        Task DeleteSingleSupplierById(Guid id);
    }
}
