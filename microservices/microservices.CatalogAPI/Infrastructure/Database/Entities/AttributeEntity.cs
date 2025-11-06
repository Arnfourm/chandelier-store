using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("Attribute")]
    public class AttributeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        public int AttributeGroupId { get; set; }

        [ForeignKey("AttributeGroupId")]
        public AttributeGroupEntity? AttributeGroup { get; set; }

        [Required]
        public int MeasurementUnitId { get; set; }

        [ForeignKey("MeasurementUnitId")]
        public MeasurementUnitEntity? MeasurementUnit { get; set; }
    }
}
