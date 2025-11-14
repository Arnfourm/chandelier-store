using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductAttributeDAO
    {
        Task<List<ProductAttribute>> GetProductAttributeByProductId(Guid productId);
        Task CreateProductAttribute(ProductAttribute productAttribute);
        Task UpdateProductAttribute(ProductAttribute productAttribute);
        Task DeleteProductAttributeByProductId(Guid productId);
        Task DeleteProductAttributeByAttributeId(Guid attributeId);
        Task DeleteProductAttributeByBothIds(Guid productId, Guid attributeId);
    }
}