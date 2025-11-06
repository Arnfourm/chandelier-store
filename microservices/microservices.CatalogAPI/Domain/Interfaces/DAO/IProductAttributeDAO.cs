using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductAttributeDAO
    {
        public Task<List<ProductAttribute>> GetProductAttributeByProductId(Guid productId);
        public Task CreateProductAttribute(ProductAttribute productAttribute);
        public Task DeleteProductAttributeByProductId(Guid productId);
        public Task UpdateProductAttribute(ProductAttribute productAttribute);
        public Task DeleteProductAttributeByAttributeId(Guid attributeId);
    }
}