using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplyProductService
    {
        private readonly ISupplyProductDAO _supplyProductDAO;

        private readonly ISupplyService _supplyService;

        public SupplyProductService(ISupplyProductDAO supplyProductDAO, ISupplyService supplyService)
        {
            _supplyProductDAO = supplyProductDAO;

            _supplyService = supplyService;
        }

        //public async Task<IEnumerable<SupplyProductResponse>> GetListSupplyProductBySupplyId(Guid supplyId)
        //{
        //    List<SupplyProduct> supplyProducts = await _supplyProductDAO.GetSupplyProductBySupplyId(supplyId);

        //    List<Guid> productIds = supplyProducts.Select(supplyProduct => supplyProduct.GetProductId()).ToList();



        //    IEnumerable<SupplyProductResponse> response = supplyProducts
        //        .Select(supplyProduct =>
        //            new SupplyProductResponse
        //            (
        //                supplyProduct.GetProductId(),
        //                supplyProduct.GetQuantity()
        //            )
        //        );
        //}
    }
}
