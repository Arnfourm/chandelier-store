using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.DAO
{
    public interface ISupplyProductDAO
    {
        Task<List<SupplyProduct>> GetSupplyProductBySupplyId(Guid supplyId);
        Task<List<SupplyProduct>> GetSupplyProductByProductId(Guid productId);
        Task CreateSupplyProduct(SupplyProduct supplyProduct);
        Task DeleteSupplyProductBySupplyId(Guid supplyId);
        Task DeleteSupplyProductByProductId(Guid productId);
        Task DeleteSupplyProductByBothIds(Guid supplyId, Guid productId);
    }
}