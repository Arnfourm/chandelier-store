using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.Contexts
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected CatalogDbContext()
        {
        }

        public DbSet<ProductTypeEntity> ProductTypes { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<AttributeGroupEntity> AttributeGroups { get; set; }
        public DbSet<MeasurementUnitEntity> MeasurementUnits { get; set; }
        public DbSet<AttributeEntity> Attributes { get; set; }
        public DbSet<ProductAttributeEntity> ProductAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttributeEntity>()
                .HasKey(pa => new {pa.ProductId, pa.AttributeId });
        }
    }
}
