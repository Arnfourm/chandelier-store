using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.OrderAPI.Infrastructure.Database.Entities
{
    [Table("OrderProduct")]
    public class OrderProductEntity
    {
        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public OrderEntity? Order { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
