using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservice.SupplyAPI.Infrastructure.Database.Entities
{
    [Table("Supply")]
    public class SupplyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public SupplierEntity? Supplier { get; set; }

        [Required]
        public DateOnly SupplyDate { get; set; }

        [Required]
        public decimal TotalAmount {  get; set; }
    }
}
