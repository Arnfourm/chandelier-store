using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class ProductTypeDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public ProductTypeDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            var productTypeList = await _catalogDbContext.ProductTypes.ToListAsync();

            if (productTypeList == null)
            {
                throw new Exception("Not a single object productType was found");
            }

            var productTypeReturn = productTypeList
                .Select(productType => new ProductType(productType.Id, productType.Title))
                .ToList();

            return productTypeReturn;
        } 

        public async Task<ProductType> GetProductTypeById(int id)
        {
            var productType = await _catalogDbContext.ProductTypes.SingleOrDefaultAsync(productType => productType.Id == id);

            if (productType == null)
            {
                throw new Exception($"Product type with id {id} not found");
            }

            return new ProductType(productType.Id, productType.Title);
        }

        public async Task<int> CreateProductType(ProductType productType)
        {
            var productTypeEntity = new ProductTypeEntity
            {
                Title = productType.GetTitle()
            };

            await _catalogDbContext.ProductTypes.AddAsync(productTypeEntity);
            await _catalogDbContext.SaveChangesAsync();

            return productTypeEntity.Id;
        }
    }
}
