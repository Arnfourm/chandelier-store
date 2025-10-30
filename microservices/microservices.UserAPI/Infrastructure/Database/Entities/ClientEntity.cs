using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("Client")]
    public class ClientEntity
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required UserEntity User { get; set; }

        [Required, NotNull]
        [StringLength(125)]
        public required string DeliveryAddressCountry { get; set; }

        [Required, NotNull]
        [StringLength(150)]
        public required string DeliveryAddressDistrict { get; set; }

        [Required, NotNull]
        [StringLength(150)]
        public required string DeliveryAddressCity { get; set; }

        [Required, NotNull]
        [StringLength(200)]
        public required string DeliveryAddressStreet { get; set; }

        [Required, NotNull]
        [StringLength(25)]
        public required string DeliveryAddressHouse { get; set; }

        [Required, NotNull]
        [StringLength(20)]
        public required string DeliveryAddressPostalIndex { get; set; }
    }
}
