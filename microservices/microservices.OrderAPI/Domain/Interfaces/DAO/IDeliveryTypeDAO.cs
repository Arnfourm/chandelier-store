using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.DAO
{
    public interface IDeliveryTypeDAO
    {
        Task<List<DeliveryType>> GetDeliveryTypes();
        Task<DeliveryType> GetDeliveryTypeById(int id);
        Task<List<DeliveryType>> GetDeliveryTypeByIds(List<int> ids);
        Task CreateDeliveryType(DeliveryType deliveryType);
        Task UpdateDeliveryType(DeliveryType deliveryType);
        Task DeleteDeliveryType(int id);
    }
}
