using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductTypeService
    {
        public Task<IEnumerable<ProductTypeResponse>> GetAllProductTypes();
        public Task<ProductType> GetSingleProductTypeById(int id);
        public Task<ProductType> GetSingleProductTypeByTitle(string title);
        Task<IEnumerable<ProductTypeResponse>> GetListProductTypeResponseByIds(List<int> ids);
        public Task<int> CreateNewProductType(ProductTypeRequest request);
        public Task<int> UpdateSingleProductType(int id, ProductTypeRequest request);
        public Task DeleteSingleProductTypeById(int id);
    }
}
