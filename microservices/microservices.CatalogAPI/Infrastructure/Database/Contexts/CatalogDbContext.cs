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


    }
}
