using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        Task<IEnumerable<SupplyResponse>> GetAllSupplies();
        Task<Supply> GetSingleSupplyById(Guid id);
        Task CreateNewSupply(SupplyRequest request);
        Task DeleteSingleSupplyById(Guid id);
    }
}