using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class ProductTypeDAO : IProductTypeDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ProductTypeDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            return await _catalogDbContext.ProductTypes
                .Select(productTypeEntity => new ProductType
                (
                    productTypeEntity.Id,
                    productTypeEntity.Title
                )).ToListAsync();
        }

        public async Task<ProductType> GetProductTypeById(int id)
        {
            var productTypeEntity = await _catalogDbContext.ProductTypes.FindAsync(id);

            if (productTypeEntity == null)
            {
                throw new Exception($"Product type with id {id} not found");
            }

            return new ProductType(productTypeEntity.Id, productTypeEntity.Title);
        }

        public async Task<ProductType> GetProductTypeByTitle(string title)
        {
            var productTypeEntity = await _catalogDbContext.ProductTypes
                .SingleOrDefaultAsync(productTypeEntity => productTypeEntity.Title == title);

            if (productTypeEntity == null)
            {
                throw new Exception($"Product type with title {title} not found");
            }

            return newProductType(productTypeEntity.Id, productTypeEntity.Title);
        }

        public async Task<int> CreateProductType(ProductType productType)
        {
            var productTypeEntity = new ProductTypeEntity
            {
                Title = productType.GetTitle()
            };

            await _catalogDbContext.ProductTypes.AddAsync(productTypeEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new product type. Error message:\n{ex.Message}", ex);
            }

            return productTypeEntity.Id;
        }

        public async Task<int> UpdateProductType(ProductType productType)
        {
            await _catalogDbContext.ProductTypes
                .Where(productTypeEntity => productTypeEntity.Id == productType.GetId())
                .ExecuteUpdateAsync(productTypeSetter => productTypeSetter
                    .SetProperty(productTypeEntity => productTypeEntity.Title, productType.GetTitle()));
        
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update product type. Error message:\n{ex.Message}", ex);
            }

            return productType.GetId();
        }

        public async Task DeleteProductTypeById(int id)
        {
            await _catalogDbContext.ProductTypes
                .Where(productTypeEntity => productTypeEntity.Id == id)
                .ExecuteDeleteAsync();

            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete product type. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
