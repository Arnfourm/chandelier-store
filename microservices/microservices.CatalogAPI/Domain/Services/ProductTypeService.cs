using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.DAO;

namespace microservices.CatalogAPI.Domain.Services
{
    public class ProductTypeService
    {
        private readonly ProductTypeDAO _productTypeDAO;

        public ProductTypeService(ProductTypeDAO productTypeDAO)
        {
            _productTypeDAO = productTypeDAO;
        }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await _productTypeDAO.GetProductTypes();

            return productTypes;
        }
    }
}
