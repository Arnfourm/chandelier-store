using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface IDeliveryTypeService
    {
        Task<IEnumerable<DeliveryTypeResponse>> GetAllDeliveryTypeResponse();
        Task<DeliveryType> GetSingleDeliveryTypeById(int id);
        Task<DeliveryTypeResponse> GetDeliveryTypeResponseByIdAsync(int id);
        Task<IEnumerable<DeliveryTypeResponse>> GetDeliveryTypeResponseByIds(List<int> ids);
        Task<DeliveryTypeResponse> CreateNewDeliveryType(DeliveryTypeRequest request);
        Task UpdateSingleDeliveryTypeById(int id, DeliveryTypeRequest request);
        Task DeleteSingleDeliveryTypeById(int id);
    }
}
