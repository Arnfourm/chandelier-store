using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("ProductType")]
    public class ProductTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, NotNull]
        [StringLength(75)]
        public required string Title { get; set; }
    }
}
