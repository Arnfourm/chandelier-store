using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.AnalysisAPI.Infrastructure.Database.Entities
{
    [Table("Order")]
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public OrderStatusEntity? Status { get; set; }

        [Required]
        public int DeliveryTypeId { get; set; }

        [ForeignKey("DeliveryTypeId")]
        public OrderDeliveryTypeEntity? DeliveryType { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
