using microservice.SupplyAPI.Domain.Models;
namespace microservice.SupplyAPI.Domain.Interfaces.DAO
{
    public interface IDeliveryTypeDAO
    {
        Task<List<DeliveryType>> GetDeliveryTypes();
        Task<DeliveryType> GetDeliveryTypeById(int id);
        Task<List<DeliveryType>> GetDeliveryTypeByIds(List<int> ids);
        Task<DeliveryType> CreateDeliveryType(DeliveryType deliveryType);
        Task UpdateDeliveryType(DeliveryType deliveryType);
        Task DeleteDeliveryTypeById(int id);
    }
}
