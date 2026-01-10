using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.AnalysisAPI.Infrastructure.Database.Entities
{
    [Table("DeliveryType")]
    public class OrderDeliveryTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(85)]
        public required string Title { get; set; }
    }
}