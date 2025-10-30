using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.DAO;

namespace microservices.CatalogAPI.Domain.Services
{
    public class ProductTypeService
    {
        private readonly IProductTypeDAO _productTypeDAO;

        public ProductTypeService(ProductType productTypeDAO)
        {
            _productTypeDAO = new productTypeDAO();
        }

        // public ProductTypeService(ProductTypeDAO productTypeDAO)
        // {
        //     _productTypeDAO = productTypeDAO;
        // }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await _productTypeDAO.GetProductTypes();

            return productTypes;
        }

        public async Task<ProductType> GetSingleProductTypeById(int id)
        {
            ProductType productType = await _productTypeDAO.GetProductTypeById();

            return productType;
        }

        public async Task<int> CreateNewProductType(ProductType productType)
        {
            int newProductTypeId = await _productTypeDAO.CreateProductType(productType);

            return newProductTypeId;
        }
    }
}
