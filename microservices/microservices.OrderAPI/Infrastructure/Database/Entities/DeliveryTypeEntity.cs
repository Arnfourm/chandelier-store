using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.OrderAPI.Infrastructure.Database.Entities
{
    [Table("DeliveryType")]
    public class DeliveryTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(85)]
        public required string Title { get; set; }
    }
}