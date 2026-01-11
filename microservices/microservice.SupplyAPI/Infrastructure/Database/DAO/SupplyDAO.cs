using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.SupplyAPI.Infrastructure.Database.DAO
{
    public class SupplyDAO : ISupplyDAO
    {
        private readonly SupplyDbContext _supplyDbContext;

        public SupplyDAO(SupplyDbContext supplyDbContext)
        {
            _supplyDbContext = supplyDbContext;
        }

        public async Task<List<Supply>> GetSupplies()
        {
            return await _supplyDbContext.Supplies
                .Select(supplyEntity => new Supply
                (
                    supplyEntity.Id,
                    supplyEntity.SupplierId,
                    supplyEntity.SupplyDate,
                    supplyEntity.TotalAmount
                )).ToListAsync();
        }

        public async Task<Supply> GetSupplyById(Guid id)
        {
            var supplyEntity = await _supplyDbContext.Supplies.FindAsync(id);

            if (supplyEntity == null)
            {
                throw new Exception($"Supplier with id {id} not found");
            }

            return new Supply
                (
                    supplyEntity.Id,
                    supplyEntity.SupplierId,
                    supplyEntity.SupplyDate,
                    supplyEntity.TotalAmount
                );
        }

        public async Task<List<Supply>> GetSupplyGyIds(List<Guid> ids)
        {
            return await _supplyDbContext.Supplies
                .Where(supplyEntity => ids.Contains(supplyEntity.Id))
                .Select(supplyEntity => new Supply
                (
                    supplyEntity.Id,
                    supplyEntity.SupplierId,
                    supplyEntity.SupplyDate,
                    supplyEntity.TotalAmount
                )).ToListAsync();
        }

        public async Task<Supply> CreateSupply(Supply supply)
        {
            SupplyEntity supplyEntity = new SupplyEntity
            {
                SupplierId = supply.GetSupplierId(),
                SupplyDate = supply.GetSupplyDate(),
                TotalAmount = supply.GetTotalAmount()
            };

            await _supplyDbContext.Supplies.AddAsync(supplyEntity);
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new supply. Error message:\n{ex.Message}", ex);
            }

            return new Supply
            (
                supplyEntity.Id,
                supplyEntity.SupplierId,
                supplyEntity.SupplyDate,
                supplyEntity.TotalAmount
            );
        }

        public async Task DeleteSupply(Guid id)
        {
            await _supplyDbContext.Supplies.Where(supplyEntity => supplyEntity.Id == id).ExecuteDeleteAsync();
            
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete supply. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
