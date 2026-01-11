using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.DAO
{
    public interface ISupplyDAO
    {
        Task<List<Supply>> GetSupplies();
        Task<Supply> GetSupplyById(Guid id);
        Task<List<Supply>> GetSupplyGyIds(List<Guid> ids);
        Task<Supply> CreateSupply(Supply supply);
        Task DeleteSupply(Guid id);
    }
}