using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierDAO _supplierDAO;

        private readonly IDeliveryTypeService _deliveryTypeService;

        public SupplierService(ISupplierDAO supplierDAO, IDeliveryTypeService deliveryTypeService)
        {
            _supplierDAO = supplierDAO;

            _deliveryTypeService = deliveryTypeService;
        }

        public async Task<IEnumerable<SupplierResponse>> GetAllSuppliers()
        {
            List<Supplier> suppliers = await _supplierDAO.GetSuppliers();

            List<int> deliveryTypeIds = suppliers.Select(supplier => supplier.GetDeliveryTypeId()).ToList();

            IEnumerable<DeliveryTypeResponse> deliverTypes = await _deliveryTypeService.GetListDeliveryTypeResponseByIds(deliveryTypeIds);

            var deliveryTypeDict = deliverTypes.ToDictionary(deliverType => deliverType.Id);

            IEnumerable<SupplierResponse> response = suppliers
                .Select(supplier =>
                    {
                        DeliveryTypeResponse deliveryType = deliveryTypeDict[supplier.GetDeliveryTypeId()];

                        return new SupplierResponse
                        (
                            supplier.GetId(),
                            supplier.GetName(),
                            deliveryType
                        );
                    });

            return response;
        }

        public async Task<Supplier> GetSingleSupplierById(Guid id)
        {
            Supplier supplier = await _supplierDAO.GetSupplierById(id);

            return supplier;
        }

        public async Task<SupplierResponse> GetSingleSupplierResponseByIdAsync(Guid id)
        {
            Supplier supplier = await _supplierDAO.GetSupplierById(id);

            DeliveryTypeResponse currentDeliveryType = await _deliveryTypeService.GetSingleDeliveryTypeResponseByIdAsync(supplier.GetDeliveryTypeId());

            return new SupplierResponse
            (
                supplier.GetId(),
                supplier.GetName(),
                currentDeliveryType
            );
        }

        public async Task<IEnumerable<SupplierResponse>> GetListSupplierResponseByIds(List<Guid> ids)
        {
            List<Supplier> suppliers = await _supplierDAO.GetSupplierByIds(ids);

            List<int> deliveryTypeIds = suppliers.Select(supplier => supplier.GetDeliveryTypeId()).ToList();

            IEnumerable<DeliveryTypeResponse> deliverTypes = await _deliveryTypeService.GetListDeliveryTypeResponseByIds(deliveryTypeIds);

            var deliveryTypeDict = deliverTypes.ToDictionary(deliverType => deliverType.Id);

            IEnumerable<SupplierResponse> response = suppliers
                .Select(supplier =>
                {
                    DeliveryTypeResponse deliveryType = deliveryTypeDict[supplier.GetDeliveryTypeId()];

                    return new SupplierResponse
                    (
                        supplier.GetId(),
                        supplier.GetName(),
                        deliveryType
                    );
                });

            return response;
        }

        public async Task<SupplierResponse> CreateNewSupplier(SupplierRequest request)
        {
            DeliveryType deliveryType = await _deliveryTypeService.GetSingleDeliveryTypeById(request.DeliveryTypeId);

            Supplier newSupplier = new Supplier
                (
                    request.Name,
                    deliveryType.GetId()
                );

            Supplier supplierResponse = await _supplierDAO.CreateSupplier(newSupplier);

            DeliveryTypeResponse currentDeliveryType = await _deliveryTypeService.GetSingleDeliveryTypeResponseByIdAsync(supplierResponse.GetDeliveryTypeId());

            return new SupplierResponse
            (
                supplierResponse.GetId(),
                supplierResponse.GetName(),
                currentDeliveryType
            );
        }


        public async Task UpdateSingleSupplier(Guid id, SupplierRequest request)
        {
            DeliveryType deliveryType = await _deliveryTypeService.GetSingleDeliveryTypeById(request.DeliveryTypeId);

            Supplier updateSupplier = new Supplier
                (
                    id,
                    request.Name,
                    deliveryType.GetId()
                );

            await _supplierDAO.UpdateSupplier(updateSupplier);
        }

        public async Task DeleteSingleSupplierById(Guid id)
        {
            await _supplierDAO.DeleteSupplierById(id);
        }
    }
}
