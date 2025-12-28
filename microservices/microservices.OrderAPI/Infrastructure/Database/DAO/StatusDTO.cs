using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Infrastructure.Database.Contexts;
using microservices.OrderAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.OrderAPI.Infrastructure.Database.DAO
{
    public class StatusDAO : IStatusDAO
    {
        private readonly OrderDbContext _orderDbContext;

        public StatusDAO(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<Status>> GetStatuses()
        {
            return await _orderDbContext.Statuses
                .Select(statusEntity => new Status
                (
                    statusEntity.Id,
                    statusEntity.Title
                )).ToListAsync();
        }

        public async Task<Status> GetStatusById(int id)
        {
            var statusEntity = await _orderDbContext.Statuses.FindAsync(id);

            if (statusEntity == null)
            {
                throw new ArgumentException($"Status with id {id} not found");
            }

            return new Status
            (
                statusEntity.Id,
                statusEntity.Title
            );
        }

        public async Task<List<Status>> GetStatusByIds(List<int> ids)
        {
            return await _orderDbContext.Statuses
                .Where(statusEntity => ids.Contains(statusEntity.Id))
                .Select(statusEntity => new Status
                (
                    statusEntity.Id,
                    statusEntity.Title
                )).ToListAsync();
        }

        public async Task<Status> CreateStatus(Status status)
        {
            StatusEntity statusEntity = new StatusEntity
            {
                Title = status.GetTitle()
            };

            await _orderDbContext.Statuses.AddAsync(statusEntity);
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to add new order status. Error message:\n{ex.Message}", ex);
            }

            return new Status(statusEntity.Id, statusEntity.Title);
        }

        public async Task UpdateStatus(Status status)
        {
            await _orderDbContext.Statuses
                .Where(statusEntity => statusEntity.Id == status.GetId())
                .ExecuteUpdateAsync(statusSetters => statusSetters
                    .SetProperty(statusEntity => statusEntity.Title, status.GetTitle()));

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to update order status. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteStatus(int id)
        {
            await _orderDbContext.Statuses.Where(statusEntity => statusEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to delete status. Error message:\n{ex.Message}", ex);
            }
        }
    }
}