using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Keyless]
    [Table("ProductAttribute")]
    public class ProductAttributeEntity
    {
        [Required, NotNull]
        public Guid ProductTypeId;

        [ForeignKey("ProductTypeId")]
        public required ProductEntity Product { get; set; }

        [Required, NotNull]
        public Guid AttributeId;

        [ForeignKey("AttributeId")]
        public required AttributeEntity Attribute { get; set; }

        [Required, NotNull]
        [StringLength(200)]
        public required string Value { get; set; }
    }
}
