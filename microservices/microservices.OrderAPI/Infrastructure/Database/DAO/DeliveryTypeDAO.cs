using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Infrastructure.Database.Contexts;
using microservices.OrderAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.OrderAPI.Infrastructure.Database.DAO
{
    public class DeliveryTypeDAO : IDeliveryTypeDAO
    {
        private readonly OrderDbContext _orderDbContext;

        public DeliveryTypeDAO(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<DeliveryType>> GetDeliveryTypes()
        {
            return await _orderDbContext.DeliveryTypes
                .Select(deliveryTypeEntity => new DeliveryType
                (
                    deliveryTypeEntity.Id,
                    deliveryTypeEntity.Title
                )).ToListAsync();
        }

        public async Task<DeliveryType> GetDeliveryTypeById(int id)
        {
            var deliveryTypeEntity = await _orderDbContext.DeliveryTypes.FindAsync(id);

            if (deliveryTypeEntity == null)
            {
                throw new ArgumentException($"Delivery type with id {id} not found");
            }

            return new DeliveryType
            (
                deliveryTypeEntity.Id,
                deliveryTypeEntity.Title
            );
        }

        public async Task<List<DeliveryType>> GetDeliveryTypeByIds(List<int> ids)
        {
            return await _orderDbContext.DeliveryTypes
                .Where(deliveryTypeEntity => ids.Contains(deliveryTypeEntity.Id))
                .Select(deliveryTypeEntity => new DeliveryType
                (
                    deliveryTypeEntity.Id,
                    deliveryTypeEntity.Title
                )).ToListAsync();
        }

        public async Task<DeliveryType> CreateDeliveryType(DeliveryType deliveryType)
        {
            DeliveryTypeEntity deliveryTypeEntity = new DeliveryTypeEntity
            {
                Title = deliveryType.GetTitle()
            };

            await _orderDbContext.DeliveryTypes.AddAsync(deliveryTypeEntity);
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to add new delivery type. Error message:\n{ex.Message}", ex);
            }

            return new DeliveryType(deliveryTypeEntity.Id, deliveryTypeEntity.Title);
        }

        public async Task UpdateDeliveryType(DeliveryType deliveryType)
        {
            await _orderDbContext.DeliveryTypes
                .Where(deliveryTypeEntity => deliveryTypeEntity.Id == deliveryType.GetId())
                .ExecuteUpdateAsync(deliveryTypeSetters => deliveryTypeSetters
                    .SetProperty(deliveryTypeEntity => deliveryTypeEntity.Title, deliveryType.GetTitle()));

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to update delivery type. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteDeliveryType(int id)
        {
            await _orderDbContext.DeliveryTypes.Where(deliveryTypeEntity => deliveryTypeEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to delete delivery type. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
