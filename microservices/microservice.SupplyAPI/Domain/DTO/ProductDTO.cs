namespace microservice.SupplyAPI.Domain.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public required string Article { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public DateOnly AddedDate { get; set; }
    }
}
