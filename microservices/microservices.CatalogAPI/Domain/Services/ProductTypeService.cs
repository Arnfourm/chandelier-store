using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeDAO _productTypeDAO;

        public ProductTypeService(IProductTypeDAO productTypeDAO)
        {
            _productTypeDAO = productTypeDAO;
        }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await _productTypeDAO.GetProductTypes();

            return productTypes;
        }

        public async Task<ProductType> GetSingleProductTypeById(int id)
        {
            ProductType productType = await _productTypeDAO.GetProductTypeById(id);

            return productType;
        }

        public async Task<int> CreateNewProductType(ProductType productType)
        {
            int newProductTypeId = await _productTypeDAO.CreateProductType(productType);

            return newProductTypeId;
        }

        public async Task<int> UpdateSingleProductType(ProductType productType)
        {
            int updatedProductTypeId = await _productTypeDAO.UpdateProductType(productType);

            return updatedProductTypeId;
        }

        public async Task DeleteSingleProductTypeById(int id)
        {
            await _productTypeDAO.DeleteProductTypeById(id);
        }
    }
}
