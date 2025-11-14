using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IDeliveryTypeDAO _deliveryTypeDAO;

        public DeliveryTypeService(IDeliveryTypeDAO deliveryTypeDAO)
        {
            _deliveryTypeDAO = deliveryTypeDAO;
        }

        public async Task<IEnumerable<DeliveryTypeResponse>> GetAllDeliveryType()
        {
            List<DeliveryType> deliveryTypes = await _deliveryTypeDAO.GetDeliveryTypes();

            IEnumerable<DeliveryTypeResponse> response = deliveryTypes
                .Select(deliveryType =>
                    new DeliveryTypeResponse
                    (
                        deliveryType.GetId(),
                        deliveryType.GetTitle(),
                        deliveryType.GetComment()
                    )
                );

            return response;
        }

        public async Task<DeliveryType> GetSingleDeliveryTypeById(int id)
        {
            DeliveryType deliveryType = await _deliveryTypeDAO.GetDeliveryTypeById(id);

            return deliveryType;
        }

        public async Task<IEnumerable<DeliveryTypeResponse>>  GetListDeliveryTypeResponseByIds(List<int> ids)
        {
            List<DeliveryType> deliveryTypes = await _deliveryTypeDAO.GetDeliveryTypeByIds(ids);

            IEnumerable<DeliveryTypeResponse> response = deliveryTypes
                .Select(deliveryType =>
                    new DeliveryTypeResponse
                    (
                        deliveryType.GetId(),
                        deliveryType.GetTitle(),
                        deliveryType.GetComment()
                    )
                );

            return response;
        }

        public async Task CreateNewDeliveryType(DeliveryTypeRequest deliveryTypeRequest)
        {
            DeliveryType newDeliveryType = new DeliveryType
                (
                    deliveryTypeRequest.Title, 
                    deliveryTypeRequest.Comment
                );

            await _deliveryTypeDAO.CreateDeliveryType(newDeliveryType);
        }

        public async Task UpdateSingleDeliveryTypeById(int id, DeliveryTypeRequest request)
        {
            DeliveryType updateDeliveryType = new DeliveryType
                (
                    id, 
                    request.Title, 
                    request.Comment
                );

            await _deliveryTypeDAO.UpdateDeliveryType(updateDeliveryType);
        }

        public async Task DeleteSingleDeliveryTypeById(int id)
        {
            await _deliveryTypeDAO.DeleteDeliveryTypeById(id);
        }
    }
}