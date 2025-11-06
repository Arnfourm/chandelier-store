using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("AttributeGroup")]
    public class AttributeGroupEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public required string Title { get; set; }
    }
}
