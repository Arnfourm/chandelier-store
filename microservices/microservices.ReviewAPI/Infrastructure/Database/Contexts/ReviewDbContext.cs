using microservices.ReviewAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.ReviewAPI.Infrastructure.Database.Contexts
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions options) : base(options)
        {
        }

        public ReviewDbContext()
        {
        }

        public DbSet<ReviewEntity> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>()
                .HasKey(r => new { r.Id });
        }
    }
}
