using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplyDeleteService : ISupplyDeleteService
    {
        private readonly ISupplyDAO _supplyDAO;

        private readonly ISupplyProductService _supplyProductService;

        public SupplyDeleteService(ISupplyDAO supplyDAO, ISupplyProductService supplyProductService)
        {
            _supplyDAO = supplyDAO;

            _supplyProductService = supplyProductService;
        }

        public async Task DeleteSingleSupplyById(Guid id)
        {
            Supply supply = await _supplyDAO.GetSupplyById(id);

            if (supply == null)
            {
                throw new Exception($"Supply with id {id} doesn't exist");
            }

            await _supplyProductService.DeleteSupplyProductBySupplyId(id);

            await _supplyDAO.DeleteSupply(id);
        }
    }
}
