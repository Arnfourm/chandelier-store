namespace microservice.SupplyAPI.Domain.DTO.Requests
{
    public class ProductRequestDTO
    {
        public required string Article { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? LampPower { get; set; }
        public int? LampCount { get; set; }
        public int ProductTypeId { get; set; }
    }
}
