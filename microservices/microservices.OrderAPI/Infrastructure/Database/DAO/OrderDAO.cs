using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Infrastructure.Database.Contexts;
using microservices.OrderAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.OrderAPI.Infrastructure.Database.DAO
{
    public class OrderDAO : IOrderDAO
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderDAO(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orderDbContext.Orders
                .Select(orderEntity => new Order
                (
                    orderEntity.Id,
                    orderEntity.UserId,
                    orderEntity.TotalAmount,
                    orderEntity.StatusId,
                    orderEntity.DeliveryTypeId,
                    orderEntity.CreationDate
                )).ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var orderEntity = await _orderDbContext.Orders.FindAsync(id);

            if (orderEntity == null)
            {
                throw new ArgumentException($"Order with id {id} not found");
            }

            return new Order
            (
                orderEntity.Id,
                orderEntity.UserId,
                orderEntity.TotalAmount,
                orderEntity.StatusId,
                orderEntity.DeliveryTypeId,
                orderEntity.CreationDate
            );
        }

        public async Task<List<Order>> GetOrderByIds(List<Guid> ids)
        {
            return await _orderDbContext.Orders
                .Where(orderEntity => ids.Contains(orderEntity.Id))
                .Select(orderEntity => new Order
                (
                    orderEntity.Id,
                    orderEntity.UserId,
                    orderEntity.TotalAmount,
                    orderEntity.StatusId,
                    orderEntity.DeliveryTypeId,
                    orderEntity.CreationDate
                )).ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            OrderEntity orderEntity = new OrderEntity
            {
                UserId = order.GetUserId(),
                TotalAmount = order.GetTotalAmount(),
                StatusId = order.GetStatusId(),
                DeliveryTypeId = order.GetDeliveryTypeId(),
                CreationDate = order.GetCreationDate(),
            };

            await _orderDbContext.Orders.AddAsync(orderEntity);
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to add new order. Error message:\n{ex.Message}", ex);
            }

            return new Order
            (
                orderEntity.Id,
                orderEntity.UserId,
                orderEntity.TotalAmount,
                orderEntity.StatusId,
                orderEntity.DeliveryTypeId,
                orderEntity.CreationDate
            );
        }

        public async Task UpdateOrder(Order order)
        {
            await _orderDbContext.Orders
                .Where(orderEntity => orderEntity.Id == order.GetId())
                .ExecuteUpdateAsync(orderSetters => orderSetters
                    .SetProperty(orderEntity => orderEntity.StatusId, order.GetStatusId())
                    .SetProperty(orderEntity => orderEntity.DeliveryTypeId, order.GetDeliveryTypeId()));

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to update order. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteOrder(Guid id)
        {
            await _orderDbContext.Orders.Where(orderEntity => orderEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to delete order. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
