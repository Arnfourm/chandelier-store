using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class ProductAttributeDAO : IProductAttributeDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ProductAttributeDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<ProductAttribute>> GetProductAttributeByProductId(Guid productId)
        {
            return await _catalogDbContext.ProductAttributes
                .Where(productAttributeEntity =>  productAttributeEntity.ProductId == productId)
                .Select(productAttributeEntity => new ProductAttribute
                (
                    productAttributeEntity.ProductId,
                    productAttributeEntity.AttributeId,
                    productAttributeEntity.Value
                )).ToListAsync();
        }

        public async Task CreateProductAttribute(ProductAttribute productAttribute)
        {
            var productAttributeEntity = new ProductAttributeEntity
            {
                ProductId = productAttribute.GetProductId(),
                AttributeId = productAttribute.GetAttributeId(),
                Value = productAttribute.GetValue()
            };

            await _catalogDbContext.ProductAttributes.AddAsync(productAttributeEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new product attribute. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteProductAttributeByProductId(Guid productId)
        {
            await _catalogDbContext.ProductAttributes
                .Where(productAttributeEntity => productAttributeEntity.ProductId == productId)
                .ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete product attribute. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteProductAttributeByAttributeId(Guid attributeId)
        {
            await _catalogDbContext.ProductAttributes
                .Where(productAttributeEntity => productAttributeEntity.AttributeId == attributeId)
                .ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete product attribute. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
