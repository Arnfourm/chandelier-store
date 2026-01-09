namespace microservice.SupplyAPI.Domain.DTO.Responses
{
    public class ProductResponseDTO
    {
        public Guid Id { get; set; }
        public required string Article { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? LampPower { get; set; }
        public int? LampCount { get; set; }
        public required ProductTypeResponseDTO ProductType { get; set; }
        public string? MainImgPath { get; set; }
        public DateOnly AddedDate { get; set; }
    }

    public class ProductTypeResponseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}