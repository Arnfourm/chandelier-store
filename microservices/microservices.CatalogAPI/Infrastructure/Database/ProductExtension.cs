using microservices.CatalogAPI.API.Filters;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database
{
    public static class ProductExtension
    {
        public static IQueryable<ProductEntity> Filter(this IQueryable<ProductEntity> query, ProductFilter filters, CatalogDbContext _catalogDbContext)
        {
            int? lamp_power_min = null;
            int? lamp_power_max = null;

            int? lamp_count_min = null;
            int? lamp_count_max = null;

            if (!string.IsNullOrEmpty(filters.lamp_power))
            {
                (lamp_power_min, lamp_power_max) = ParseRange(filters.lamp_power);
            }

            if (!string.IsNullOrEmpty(filters.lamp_count))
            {
                (lamp_count_min, lamp_count_max) = ParseRange(filters.lamp_count);
            }

            if (filters.price_min.HasValue)
            {
                query = query.Where(p => p.Price >= filters.price_min);
            }

            if (filters.price_max.HasValue)
            {
                query = query.Where(p => p.Price <= filters.price_max);
            }

            if (!string.IsNullOrWhiteSpace(filters.product_type))
            {
                query = query
                    .Include(p => p.ProductType)
                    .Where(p => p.ProductType.Title.ToLower() == filters.product_type.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filters.room_type))
            {
                query = query
                    .Where(p => _catalogDbContext.ProductAttributes
                        .Include(pa => pa.Attribute)
                        .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "помещение" && pa.Value.ToLower() == filters.room_type.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filters.color))
            {
                query = query
                    .Where(p => _catalogDbContext.ProductAttributes
                        .Include(pa => pa.Attribute)
                        .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "цвет" && pa.Value.ToLower() == filters.color.ToLower()));
            }

            //if (lamp_power_min.HasValue)
            //{
            //    query = query
            //        .Where(p => _catalogDbContext.ProductAttributes
            //            .Include(pa => pa.Attribute)
            //            .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "мощность ламп" && Convert.ToInt32(pa.Value) >= lamp_power_min));
            //}

            //if (lamp_power_max.HasValue)
            //{
            //    query = query
            //        .Where(p => _catalogDbContext.ProductAttributes
            //            .Include(pa => pa.Attribute)
            //            .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "мощность ламп" && Convert.ToInt32(pa.Value) <= lamp_power_max));
            //}

            //if (lamp_count_min.HasValue)
            //{
            //    query = query
            //        .Where(p => _catalogDbContext.ProductAttributes
            //            .Include(pa => pa.Attribute)
            //            .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "количество ламп" && Convert.ToInt32(pa.Value) >= lamp_count_min));
            //}

            //if (lamp_count_max.HasValue)
            //{
            //    query = query
            //        .Where(p => _catalogDbContext.ProductAttributes
            //            .Include(pa => pa.Attribute)
            //            .Any(pa => pa.ProductId == p.Id && pa.Attribute.Title.ToLower() == "количество ламп" && Convert.ToInt32(pa.Value) <= lamp_count_max));
            //}

            return query;
        }

        public static IQueryable<ProductEntity> Sort(this IQueryable<ProductEntity> query, string? sort)
        {
            switch (sort?.ToLower())
            {
                case "price-down":
                    query = query.OrderByDescending(productEntity => productEntity.Price);
                    break;
                case "price-up":
                    query = query.OrderBy(productEntity => productEntity.Price);
                    break;
                case "new":
                    query = query.OrderByDescending(productEntity => productEntity.AddedDate);
                    break;
                // Made as "Popular" via special attribute in Product model "Sells per month"
                default:
                    query = query.OrderBy(productEntiy => productEntiy.Title.ToLower());
                    break;
            }

            return query;
        }

        // Searc by product title (upgrade in the future)
        public static IQueryable<ProductEntity> Search(this IQueryable<ProductEntity> query, string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(search.ToLower()));
            }

            return query;
        }

        private static (int? min, int? max) ParseRange(string range)
        {
            if (string.IsNullOrWhiteSpace(range) || !range.Contains("-"))
            {
                return (null, null);
            }

            string[] parts = range.Split('-', StringSplitOptions.RemoveEmptyEntries);

            int? min = int.TryParse(parts[0].Trim(), out var tmpMin) ? tmpMin : null;
            int? max = int.TryParse(parts[1].Trim(), out var tmpMax) ? tmpMax : null;

            return (min, max);
        }
    }
}
