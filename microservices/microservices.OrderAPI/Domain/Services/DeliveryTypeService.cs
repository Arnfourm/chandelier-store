using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Services
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        public readonly IDeliveryTypeDAO _deliveryTypeDAO;

        public DeliveryTypeService(IDeliveryTypeDAO deliveryTypeDAO)
        {
            _deliveryTypeDAO = deliveryTypeDAO;
        }

        public async Task<IEnumerable<DeliveryTypeResponse>> GetAllDeliveryTypeResponse()
        {
            List<DeliveryType> deliveryTypes = await _deliveryTypeDAO.GetDeliveryTypes();

            IEnumerable<DeliveryTypeResponse> response = deliveryTypes
                .Select(deliveryType => new DeliveryTypeResponse
                    (
                        deliveryType.GetId(),
                        deliveryType.GetTitle()
                    )
                );

            return response;
        }

        public async Task<DeliveryType> GetSingleDeliveryTypeById(int id)
        {
            DeliveryType deliveryType = await _deliveryTypeDAO.GetDeliveryTypeById(id);

            return deliveryType;
        }

        public async Task<DeliveryTypeResponse> GetDeliveryTypeResponseByIdAsync(int id)
        {
            DeliveryType deliveryType = await _deliveryTypeDAO.GetDeliveryTypeById(id);

            DeliveryTypeResponse deliveryTypeResponse = new DeliveryTypeResponse
            (
                deliveryType.GetId(),
                deliveryType.GetTitle()
            );

            return deliveryTypeResponse;
        }

        public async Task<IEnumerable<DeliveryTypeResponse>> GetDeliveryTypeResponseByIds(List<int> ids)
        {
            List<DeliveryType> deliveryTypes = await _deliveryTypeDAO.GetDeliveryTypeByIds(ids);

            IEnumerable<DeliveryTypeResponse> response = deliveryTypes
                .Select(deliveryType => new DeliveryTypeResponse
                    (
                        deliveryType.GetId(),
                        deliveryType.GetTitle()
                    )
                );

            return response;
        }

        public async Task<DeliveryTypeResponse> CreateNewDeliveryType(DeliveryTypeRequest request)
        {
            DeliveryType newDeliveryType = new DeliveryType
            (
                request.Title
            );

            DeliveryType responseDeliveryType = await _deliveryTypeDAO.CreateDeliveryType(newDeliveryType);

            return new DeliveryTypeResponse(responseDeliveryType.GetId(), responseDeliveryType.GetTitle());
        }

        public async Task UpdateSingleDeliveryTypeById(int id, DeliveryTypeRequest request)
        {
            DeliveryType updateDeliveryType = new DeliveryType
            (
                id,
                request.Title
            );

            await _deliveryTypeDAO.UpdateDeliveryType(updateDeliveryType);
        }

        public async Task DeleteSingleDeliveryTypeById(int id)
        {
            await _deliveryTypeDAO.DeleteDeliveryType(id);
        }
    }
}
