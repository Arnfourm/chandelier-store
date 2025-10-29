using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("Attribute")]
    public class AttributeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, NotNull]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required, NotNull]
        public int AttributeGroupId { get; set; }

        [ForeignKey("AttributeGroupId")]
        public required AttributeGroupEntity AttributeGroup { get; set; }

        [Required, NotNull]
        public int MeasurementUnitId { get; set; }

        [ForeignKey("MeasurementUnitId")]
        public required MeasurementUnitEntity MeasurementUnit { get; set; }
    }
}
