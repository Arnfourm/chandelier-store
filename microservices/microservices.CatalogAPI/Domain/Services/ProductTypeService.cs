using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
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

        public async Task<IEnumerable<ProductTypeResponse>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await _productTypeDAO.GetProductTypes();

            IEnumerable<ProductTypeResponse> response = productTypes
                .Select(productType =>
                    new ProductTypeResponse(productType.GetId(), productType.GetTitle()));

            return response;
        }

        public async Task<ProductType> GetSingleProductTypeById(int id)
        {
            ProductType productType = await _productTypeDAO.GetProductTypeById(id);

            return productType;
        }

        public async Task<ProductTypeResponse> GetSingleProductTypeResponseById(int id)
        {
            ProductType productType = await _productTypeDAO.GetProductTypeById(id);

            ProductTypeResponse response = new ProductTypeResponse
            (
                productType.GetId(),
                productType.GetTitle()
            );

            return response;
        }

        public async Task<IEnumerable<ProductTypeResponse>> GetListProductTypeResponseByIds(List<int> ids)
        {
            List<ProductType> productTypes = await _productTypeDAO.GetProductTypeByIds(ids);

            IEnumerable<ProductTypeResponse> productTypeResponse = productTypes
                .Select(productType =>
                    new ProductTypeResponse(productType.GetId(), productType.GetTitle()));

            return productTypeResponse;
        }

        public async Task<ProductType> GetSingleProductTypeByTitle(string title)
        {
            ProductType productType = await _productTypeDAO.GetProductTypeByTitle(title);

            return productType;
        }

        public async Task<int> CreateNewProductType(ProductTypeRequest request)
        {
            ProductType newProductType = new ProductType(request.Title); 

            int newProductTypeId = await _productTypeDAO.CreateProductType(newProductType);

            return newProductTypeId;
        }

        public async Task<int> UpdateSingleProductType(int id, ProductTypeRequest request)
        {
            ProductType updateProductType = new ProductType(id, request.Title);

            int updatedProductTypeId = await _productTypeDAO.UpdateProductType(updateProductType);

            return updatedProductTypeId;
        }

        public async Task DeleteSingleProductTypeById(int id)
        {
            await _productTypeDAO.DeleteProductTypeById(id);
        }
    }
}
