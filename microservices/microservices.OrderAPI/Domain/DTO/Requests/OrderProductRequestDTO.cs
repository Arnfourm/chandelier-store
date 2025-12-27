namespace microservices.OrderAPI.Domain.DTO.Requests
{
    public class OrderProductRequestDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
    }
}