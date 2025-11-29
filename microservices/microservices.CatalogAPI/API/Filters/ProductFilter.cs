namespace microservices.CatalogAPI.API.Filters
{
    public class ProductFilter
    {
        public string? product_type { get; set; }
        public int? price_min { get; set; }
        public int? price_max { get; set; }
        public string? room_type { get; set; }
        public string? color { get; set; }
        public string? lamp_power { get; set; }
        public string? lamp_count { get; set; }
    }
}
