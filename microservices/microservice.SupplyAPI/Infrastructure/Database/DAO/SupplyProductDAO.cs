using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.SupplyAPI.Infrastructure.Database.DAO
{
    public class SupplyProductDAO : ISupplyProductDAO
    {
        private readonly SupplyDbContext _supplyDbContext;

        public SupplyProductDAO(SupplyDbContext supplyDbContext)
        {
            _supplyDbContext = supplyDbContext;
        }

        public async Task<List<SupplyProduct>> GetSupplyProductBySupplyId(Guid supplyId)
        {
            return await _supplyDbContext.SupplyProducts
                .Where(supplyProductEntity => supplyProductEntity.SupplyId == supplyId)
                .Select(supplyProductEntity => new SupplyProduct
                (
                    supplyProductEntity.ProductId,
                    supplyProductEntity.SupplyId,
                    supplyProductEntity.Quantity
                )).ToListAsync();
        }

        public async Task<List<SupplyProduct>> GetSupplyProductByProductId(Guid productId)
        {
            return await _supplyDbContext.SupplyProducts
                .Where(supplyProductEntity => supplyProductEntity.ProductId == productId)
                .Select(supplyProductEntity => new SupplyProduct
                (
                    supplyProductEntity.ProductId,
                    supplyProductEntity.SupplyId,
                    supplyProductEntity.Quantity
                )).ToListAsync();
        }

        public async Task CreateSupplyProduct(SupplyProduct supplyProduct)
        {
            SupplyProductEntity supplyProductEntity = new SupplyProductEntity
            {
                SupplyId = supplyProduct.GetSupplyId(),
                ProductId = supplyProduct.GetProductId(),
                Quantity = supplyProduct.GetQuantity()
            };

            await _supplyDbContext.SupplyProducts.AddAsync(supplyProductEntity);
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new supply product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteSupplyProductBySupplyId(Guid supplyId)
        {
            await _supplyDbContext.SupplyProducts
                .Where(supplyProductEntity => supplyProductEntity.SupplyId == supplyId)
                .ExecuteDeleteAsync();
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete supply product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteSupplyProductByProductId(Guid productId)
        {
            await _supplyDbContext.SupplyProducts
                .Where(supplyProductEntity => supplyProductEntity.ProductId == productId)
                .ExecuteDeleteAsync();
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete supply product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteSupplyProductByBothIds(Guid supplyId, Guid productId)
        {
            await _supplyDbContext.SupplyProducts
                .Where(supplyProductEntity => supplyProductEntity.SupplyId == supplyId && supplyProductEntity.ProductId == productId)
                .ExecuteDeleteAsync();
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete supply product. Error message:\n{ex.Message}", ex);
            }
        }
    }
}