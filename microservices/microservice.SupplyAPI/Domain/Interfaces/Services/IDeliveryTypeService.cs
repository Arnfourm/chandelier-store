using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Interfaces.Services
{
    public interface IDeliveryTypeService
    {
        Task<IEnumerable<DeliveryTypeResponse>> GetAllDeliveryType();
        Task<DeliveryType> GetSingleDeliveryTypeById(int id);
        Task<IEnumerable<DeliveryTypeResponse>> GetListDeliveryTypeResponseByIds(List<int> ids);
        Task CreateNewDeliveryType(DeliveryTypeRequest deliveryTypeRequest);
        Task UpdateSingleDeliveryTypeById(int id, DeliveryTypeRequest request);
        Task DeleteSingleDeliveryTypeById(int id);
    }
}
