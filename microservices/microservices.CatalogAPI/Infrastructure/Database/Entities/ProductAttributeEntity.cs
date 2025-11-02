using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Keyless]
    [Table("ProductAttribute")]
    public class ProductAttributeEntity
    {
        [Required]
        public Guid ProductId;

        [ForeignKey("ProductId")]
        public ProductEntity? Product { get; set; }

        [Required]
        public Guid AttributeId;

        [ForeignKey("AttributeId")]
        public AttributeEntity? Attribute { get; set; }

        [Required]
        [StringLength(200)]
        public required string Value { get; set; }
    }
}
