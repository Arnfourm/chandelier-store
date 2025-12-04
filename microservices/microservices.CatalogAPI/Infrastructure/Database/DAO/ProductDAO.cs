using microservices.CatalogAPI.API.Filters;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class ProductDAO : IProductDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ProductDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<Product>> GetProducts(
                string? sort,
                ProductFilter filters,
                string? search
            )
        {
            var query = _catalogDbContext.Products
                .Filter(filters, _catalogDbContext)
                .Sort(sort)
                .Search(search)
                .AsQueryable()
                .AsNoTracking();

            return await query
                .Select(productEntity => new Product
                (
                    productEntity.Id,
                    productEntity.Article,
                    productEntity.Title,
                    productEntity.Price,
                    productEntity.Quantity,
                    productEntity.LampPower,
                    productEntity.LampCount,
                    productEntity.ProductTypeId,
                    productEntity.AddedDate
                )).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var productEntity = await _catalogDbContext.Products.FindAsync(id);

            if (productEntity == null)
            {
                throw new Exception($"Product with id {id} not found");
            }

            return new Product(
                productEntity.Id,
                productEntity.Article,
                productEntity.Title,
                productEntity.Price,
                productEntity.Quantity,
                productEntity.LampPower,
                productEntity.LampCount,
                productEntity.ProductTypeId,
                productEntity.AddedDate);
        }
        
        public async Task<List<Product>> GetProductsByIds(List<Guid> ids)
        {
            return await _catalogDbContext.Products
                .Where(productEntity => ids.Contains(productEntity.Id))
                .Select(productEntity => new Product
                (
                    productEntity.Id,
                    productEntity.Article,
                    productEntity.Title,
                    productEntity.Price,
                    productEntity.Quantity,
                    productEntity.LampPower,
                    productEntity.LampCount,
                    productEntity.ProductTypeId,
                    productEntity.AddedDate
                )).ToListAsync();
        }

        public async Task<Guid> CreateProduct(Product product)
        {
            var productEntity = new ProductEntity
            {
                Article = product.GetArticle(),
                Title = product.GetTitle(),
                Price = product.GetPrice(),
                Quantity = product.GetQuantity(),
                LampPower = product.GetLampPower(),
                LampCount = product.GetLampCount(),
                ProductTypeId = product.GetProductTypeId(),
                AddedDate = product.GetAddedDate(),
            };

            await _catalogDbContext.AddAsync(productEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new product. Error message:\n{ex.Message}", ex);
            }

            return productEntity.Id;
        }

        public async Task<Guid> UpdateProduct(Product product)
        {
            await _catalogDbContext.Products
                .Where(productEntity => productEntity.Id == product.GetId())
                .ExecuteUpdateAsync(productSetters => productSetters
                    .SetProperty(productEntity => productEntity.Article, product.GetArticle())
                    .SetProperty(productEntity => productEntity.Title, product.GetTitle())
                    .SetProperty(productEntity => productEntity.Price, product.GetPrice())
                    .SetProperty(productEntity => productEntity.Quantity, product.GetQuantity())
                    .SetProperty(productEntity => productEntity.LampPower, product.GetLampPower())
                    .SetProperty(productEntity => productEntity.LampCount, product.GetLampCount())
                    .SetProperty(productEntity => productEntity.ProductTypeId, product.GetProductTypeId()));

            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update general info product. Error message:\n{ex.Message}", ex);
            }

            return product.GetId();
        }

        public async Task DeleteProductById(Guid id)
        {
            await _catalogDbContext.Products
                .Where(productEntity => productEntity.Id == id)
                .ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete product. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
