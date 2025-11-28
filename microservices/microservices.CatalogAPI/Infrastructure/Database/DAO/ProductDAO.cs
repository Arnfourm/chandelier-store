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
                string? product_type,
                int? price_min,
                int? price_max,
                string? room_type,
                string? color,
                string? lamp_power,
                string? lamp_count
            )
        {
            var query = _catalogDbContext.Products.AsQueryable().AsNoTracking();

            // == ФИЛЬТРАЦИЯ ==
            if (price_min.HasValue)
            {
                query = query.Where(p => p.Price >= price_min);
            }

            if (price_max.HasValue)
            {
                query = query.Where(p => p.Price <= price_max);
            }

            if (!string.IsNullOrWhiteSpace(product_type))
            {
                query = query
                    .Include(p => p.ProductType)
                    .Where(p => p.ProductType.Title.ToLower() == product_type.ToLower());
            }

            //if (!string.IsNullOrWhiteSpace(room_type))
            //{
            //    query = query
            //        .Where(p => _catalogDbContext.ProductAttributes
            //        .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == room_type);

            //}


            // == СОРТИРОВКА ==
            switch (sort?.ToLower())
            {
                case "price-down":
                    query = query.OrderByDescending(productEntity => productEntity.Price);
                    break;
                case "price-up":
                    query = query.OrderBy(productEntity => productEntity.Price);
                    break;
                case "new":
                    query = query.OrderByDescending(productEntity => productEntity.AddedDate);
                    break;
                // Сделать по популярности по популярности через параметр объекта (продаж в месяц)
                default:
                    query = query.OrderBy(productEntiy => productEntiy.Title.ToLower());
                    break;
            }


            return await query
                .Select(productEntity => new Product
                (
                    productEntity.Id,
                    productEntity.Article,
                    productEntity.Title,
                    productEntity.Price,
                    productEntity.Quantity,
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
