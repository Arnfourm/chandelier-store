using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Infrastructure.Database.Contexts;
using microservices.OrderAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.OrderAPI.Infrastructure.Database.DAO
{
    public class OrderProductDAO : IOrderProductDAO
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderProductDAO(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<OrderProduct>> GetOrderProductByOrderId(Guid orderId)
        {
            return await _orderDbContext.OrderProducts
                .Where(orderProductEntity => orderProductEntity.OrderId == orderId)
                .Select(orderProductEntity => new OrderProduct
                (
                    orderProductEntity.OrderId,
                    orderProductEntity.ProductId,
                    orderProductEntity.UnitPrice,
                    orderProductEntity.Quantity
                )).ToListAsync();
        }

        public async Task<List<OrderProduct>> GetOrderProductByProductId(Guid productId)
        {
            return await _orderDbContext.OrderProducts
                .Where(orderProductEntity => orderProductEntity.ProductId == productId)
                .Select(orderProductEntity => new OrderProduct
                (
                    orderProductEntity.OrderId,
                    orderProductEntity.ProductId,
                    orderProductEntity.UnitPrice,
                    orderProductEntity.Quantity
                )).ToListAsync();
        }

        public async Task CreateOrderProduct(OrderProduct orderProduct)
        {
            OrderProductEntity orderProductEntity = new OrderProductEntity
            {
                OrderId = orderProduct.GetOrderId(),
                ProductId = orderProduct.GetProductId(),
                UnitPrice = orderProduct.GetUnitPrice(),
                Quantity = orderProduct.GetQuantity()
            };

            await _orderDbContext.OrderProducts.AddAsync(orderProductEntity);
            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new order product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteOrderProductByOrderId(Guid orderId)
        {
            await _orderDbContext.OrderProducts
                .Where(orderProductEntity => orderProductEntity.OrderId == orderId)
                .ExecuteDeleteAsync();

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete order product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteOrderProductByProductId(Guid productId)
        {
            await _orderDbContext.OrderProducts
                .Where(orderProductEntity => orderProductEntity.ProductId == productId)
                .ExecuteDeleteAsync();

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete order product. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteOrderProductByBothIds(Guid orderId, Guid productId)
        {
            await _orderDbContext.OrderProducts
                .Where(orderProductEntity => orderProductEntity.OrderId == orderId && orderProductEntity.ProductId == productId)
                .ExecuteDeleteAsync();

            try
            {
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete order product. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
