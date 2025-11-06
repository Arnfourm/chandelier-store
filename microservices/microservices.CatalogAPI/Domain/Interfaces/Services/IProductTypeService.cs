using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductTypeService
    {
        public Task<List<ProductType>> GetAllProductTypes();
        public Task<ProductType> GetSingleProductTypeById(int id);
        public Task<int> CreateNewProductType(ProductType productType);
        public Task<int> UpdateSingleProductType(ProductType productType);
        public Task DeleteSingleProductTypeById(int id);
    }
}
