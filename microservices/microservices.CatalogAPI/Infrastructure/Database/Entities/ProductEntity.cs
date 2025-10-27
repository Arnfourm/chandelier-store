using Microsoft.EntityFrameworkCore;
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
        [StringLength(75)]
        public required string Article { get; set; }

        [Required]
        [StringLength(200)]
        public required string Name { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
