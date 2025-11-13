using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservice.SupplyAPI.Infrastructure.Database.Entities
{
    [Table("SupplyProduct")]
    public class SupplyProductEntity
    {
        [Required]
        public Guid SupplyId { get; set; }

        [ForeignKey("SupplyId")]
        public SupplyEntity? Supply { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}
