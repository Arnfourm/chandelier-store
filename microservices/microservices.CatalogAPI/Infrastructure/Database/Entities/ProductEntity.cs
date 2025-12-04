using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Article { get; set; }

        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int? LampPower { get; set; }

        public int? LampCount { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductTypeEntity? ProductType { get; set; }

        public string? MainImgPath { get; set; }

        [Required]
        public DateOnly AddedDate { get; set; }
    }
}
