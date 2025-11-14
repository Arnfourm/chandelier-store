using microservice.SupplyAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.SupplyAPI.Infrastructure.Database.Contexts
{
    public class SupplyDbContext : DbContext
    {
        public SupplyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SupplyDbContext()
        {
        }

        public DbSet<DeliveryTypeEntity> DeliveryTypes { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SupplyEntity> Supplies { get; set; }
        public DbSet<SupplyProductEntity> SupplyProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplyProductEntity>()
                .HasKey(sp => new { sp.SupplyId, sp.ProductId });
        }
    }
}
