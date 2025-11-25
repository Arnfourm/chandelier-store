using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductTypeResponse>> GetAllProductTypes();
        Task<ProductType> GetSingleProductTypeById(int id);
        Task<ProductType> GetSingleProductTypeByTitle(string title);
        Task<ProductTypeResponse> GetSingleProductTypeResponseById(int id);
        Task<IEnumerable<ProductTypeResponse>> GetListProductTypeResponseByIds(List<int> ids);
        Task<int> CreateNewProductType(ProductTypeRequest request);
        Task<int> UpdateSingleProductType(int id, ProductTypeRequest request);
        Task DeleteSingleProductTypeById(int id);
    }
}
