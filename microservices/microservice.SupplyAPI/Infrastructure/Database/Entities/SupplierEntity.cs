using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservice.SupplyAPI.Infrastructure.Database.Entities
{
    [Table("Supplier")]
    public class SupplierEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public required string Name { get; set; }

        [Required]
        public int DeliveryTypeId { get; set; }

        [ForeignKey("eliveryTypeId")]
        public DeliveryTypeEntity? DeliveryType { get; set; }
    }
}
