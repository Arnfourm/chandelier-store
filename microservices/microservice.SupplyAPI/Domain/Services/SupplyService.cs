using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly ISupplyDAO _supplyDAO;

        private readonly ISupplierService _supplierService;

        public SupplyService(ISupplyDAO supplyDAO, ISupplierService supplierService)
        {
            _supplyDAO = supplyDAO;

            _supplierService = supplierService;
        }

        public async Task<IEnumerable<SupplyResponse>> GetAllSupplies()
        {
            List<Supply> supplies = await _supplyDAO.GetSupplies();

            List<Guid> supplierIds = supplies.Select(supply => supply.GetSupplierId()).ToList();

            IEnumerable<SupplierResponse> suppliers = await _supplierService.GetListSupplierResponseByIds(supplierIds);

            var supplierDict = suppliers.ToDictionary(supplier => supplier.Id);

            IEnumerable<SupplyResponse> response = supplies
                .Select(supply =>
                {
                    SupplierResponse supplierResponse = supplierDict[supply.GetSupplierId()];

                    return new SupplyResponse
                    (
                        supply.GetId(),
                        supplierResponse,
                        supply.GetSupplyDate(),
                        supply.GetTotalAmount()
                    );
                });

            return response;
        }

        public async Task<Supply> GetSingleSupplyById(Guid id)
        {
            Supply supply = await _supplyDAO.GetSupplyById(id);

            return supply;
        }

        public async Task<SupplyResponse> CreateNewSupply(SupplyRequest request)
        {
            Supply newSupply = new Supply
                (
                    request.SupplierId,
                    DateOnly.FromDateTime(DateTime.Now),
                    request.TotalAmount
                );

            Supply supplyResponse = await _supplyDAO.CreateSupply(newSupply);

            SupplierResponse currentSupplier = await _supplierService.GetSingleSupplierResponseByIdAsync(supplyResponse.GetSupplierId());

            return new SupplyResponse
            (
                supplyResponse.GetId(),
                currentSupplier,
                supplyResponse.GetSupplyDate(),
                supplyResponse.GetTotalAmount()
            );
        }
    }
}
