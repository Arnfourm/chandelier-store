using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.CatalogAPI.Infrastructure.Database.Entities
{
    [Table("LightingDevice")]
    public class LightingDeviceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
	
	[Required]
	public LightingDeviceType LightingDeviceType { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Weight { get; set; }

	public Color Color { get; set; }
        
	public BulbType BulbType { get; set; }

	public int BulbCount { get; set; }

        [Required]
        [StringLength(75)]
        public required string Article { get; set; }

    }
}
