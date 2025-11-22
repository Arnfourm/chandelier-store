using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;

namespace microservice.SupplyAPI.Domain.Interfaces.Services
{
    public interface ISupplyProductService
    {
        Task<IEnumerable<SupplyProductResponse>> GetListSupplyProductBySupplyId(Guid supplyId);
        Task CreateNewSupplyProduct(SupplyProductRequest request);
    }
}
