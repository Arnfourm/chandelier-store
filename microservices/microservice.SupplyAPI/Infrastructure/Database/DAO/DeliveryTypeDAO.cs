using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.SupplyAPI.Infrastructure.Database.DAO
{
    public class DeliveryTypeDAO : IDeliveryTypeDAO
    {
        private readonly SupplyDbContext _supplyDbContext;

        public DeliveryTypeDAO(SupplyDbContext supplyDbContext)
        {
            _supplyDbContext = supplyDbContext;
        }

        public async Task<List<DeliveryType>> GetDeliveryTypes()
        {
            return await _supplyDbContext.DeliveryTypes
                .Select(deliveryTypeEntity => new DeliveryType
                (
                    deliveryTypeEntity.Id,
                    deliveryTypeEntity.Title,
                    deliveryTypeEntity.Comment
                )).ToListAsync();
        }

        public async Task<DeliveryType> GetDeliveryTypeById(int id)
        {
            var deliveryTypeEntity = await _supplyDbContext.DeliveryTypes.FindAsync(id);

            if (deliveryTypeEntity == null)
            {
                throw new Exception($"Delivery type with id {id} not found");
            }

            return new DeliveryType
                (
                    deliveryTypeEntity.Id, 
                    deliveryTypeEntity.Title, 
                    deliveryTypeEntity.Comment
                );
        }

        public async Task<List<DeliveryType>> GetDeliveryTypeByIds(List<int> ids)
        {
            return await _supplyDbContext.DeliveryTypes
                .Where(deliveryTypeEntity => ids.Contains(deliveryTypeEntity.Id))
                .Select(deliveryTypeEntity => new DeliveryType
                (
                    deliveryTypeEntity.Id,
                    deliveryTypeEntity.Title,
                    deliveryTypeEntity.Comment
                )).ToListAsync();
        }

        public async Task<DeliveryType> CreateDeliveryType(DeliveryType deliveryType)
        {
            DeliveryTypeEntity deliveryTypeEntity = new DeliveryTypeEntity
            {
                Title = deliveryType.GetTitle(),
                Comment = deliveryType.GetComment()
            };

            await _supplyDbContext.DeliveryTypes.AddAsync(deliveryTypeEntity);
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new delivery type. Error message:\n{ex.Message}", ex);
            }

            return new DeliveryType
            (
                deliveryTypeEntity.Id,
                deliveryTypeEntity.Title,
                deliveryTypeEntity.Comment
            );
        }
        
        public async Task UpdateDeliveryType(DeliveryType deliveryType)
        {
            await _supplyDbContext.DeliveryTypes
                .Where(deliveryTypeEntity => deliveryTypeEntity.Id == deliveryType.GetId())
                .ExecuteUpdateAsync(deliveryTypeSetters => deliveryTypeSetters
                    .SetProperty(deliveryTypeEntity => deliveryTypeEntity.Title, deliveryType.GetTitle())
                    .SetProperty(deliveryTypeEntity => deliveryTypeEntity.Comment, deliveryType.GetComment()));

            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update delivery type. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteDeliveryTypeById(int id)
        {
            await _supplyDbContext.DeliveryTypes.Where(deliveryTypeEntity => deliveryTypeEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete delivery type. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
