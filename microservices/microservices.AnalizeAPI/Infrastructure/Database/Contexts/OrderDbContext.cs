using microservices.AnalysisAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.AnalysisAPI.Infrastructure.Database.Contexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        protected OrderDbContext()
        {
        }

        public DbSet<OrderStatusEntity> Statuses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderProductEntity> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProductEntity>()
                .HasKey(op => new { op.OrderId, op.ProductId });
        }
    }
}
