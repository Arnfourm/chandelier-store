using microservices.AnalizeAPI.Domain.Interfaces.DAO;
using microservices.AnalizeAPI.Domain.Models;
using microservices.AnalysisAPI.Infrastructure.Database.Contexts;
using microservices.AnalysisAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace microservices.AnalizeAPI.Infrastructure.Database.DAO
{
    public class OrderDAO : IOrderDAO
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderDAO(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<OrderStats>> GetOrdersAsync(DateTime? from, DateTime? to)
        {
            IQueryable<OrderEntity> queryOrder = _orderDbContext.Orders;
            
            if (from.HasValue)
            {
                queryOrder = queryOrder.Where(orderEntity => orderEntity.CreationDate.Date >= from.Value.Date);
            }
            if (to.HasValue)
            {
                queryOrder = queryOrder.Where(orderEntity => orderEntity.CreationDate.Date <= to.Value.Date);
            }

            var queryOrderStats = queryOrder
                .GroupBy(order => order.CreationDate.Date)
                .Select(orderGroup => new OrderStats
                (
                    orderGroup.Key,
                    orderGroup.Sum(order => order.TotalAmount),
                    orderGroup.Count(),
                    orderGroup.Average(order => order.TotalAmount)
                ));

            return await queryOrderStats
                .ToListAsync();
        }
    }
}
