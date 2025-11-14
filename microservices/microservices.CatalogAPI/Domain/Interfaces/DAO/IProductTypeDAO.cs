using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductTypeDAO
    {
        Task<List<ProductType>> GetProductTypes();
        Task<ProductType> GetProductTypeById(int id);
        Task<List<ProductType>> GetProductTypeByIds(List<int> ids);
        Task<ProductType> GetProductTypeByTitle(string title);
        Task<int> CreateProductType(ProductType productType);
        Task<int> UpdateProductType(ProductType productType);
        Task DeleteProductTypeById(int id);
    }
}