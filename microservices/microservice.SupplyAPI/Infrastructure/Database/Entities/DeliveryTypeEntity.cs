using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservice.SupplyAPI.Infrastructure.Database.Entities
{
    [Table("DeliveryType")]
    public class DeliveryTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public required string Title { get; set; }

        [Required]
        [StringLength(350)]
        public string Comment { get; set; }
    }
}
