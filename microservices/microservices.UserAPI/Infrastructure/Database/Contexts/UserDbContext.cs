using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.Contexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        protected UserDbContext()
        {
        }

        public DbSet<PasswordEntity> Passwords { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<FavoritesEntity> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>()
                .HasKey(c => new { c.UserId });

            modelBuilder.Entity<EmployeeEntity>()
               .HasKey(e => new { e.UserId });

            modelBuilder.Entity<FavoritesEntity>()
                .HasKey(c => new { c.UserId, c.ProductId });
        }
    }
}
