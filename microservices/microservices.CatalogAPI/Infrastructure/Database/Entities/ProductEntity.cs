using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, NotNull]
        [StringLength(100)]
        public required string Article { get; set; }

        [Required, NotNull]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required, NotNull]
        public decimal Price { get; set; }

        [Required, NotNull]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public required ProductTypeEntity ProductType { get; set; }

        [Required, NotNull]
        public DateOnly AddedDate { get; set; }
    }
}
