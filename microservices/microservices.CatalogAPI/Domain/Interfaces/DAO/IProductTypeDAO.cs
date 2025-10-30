using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductTypeDAO
    {
        public Task<List<ProductType>> GetProductTypes();
        public Task<ProductType> GetProductTypeById(int id);
        public Task<int> CreateProductType(ProductType productType);
    }
}