using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("Employee")]
    public class EmployeeEntity
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required UserEntity User { get; set; }

        [Required, NotNull]
        public int Code { get; set; }

        [Required, NotNull]
        [StringLength(125)]
        public required string ResidenceAddressCountry { get; set; }

        [Required, NotNull]
        [StringLength(150)]
        public required string ResidenceAddressDistrict { get; set; }

        [Required, NotNull]
        [StringLength(150)]
        public required string ResidenceAddressCity { get; set; }

        [Required, NotNull]
        [StringLength(200)]
        public required string ResidenceAddressStreet { get; set; }

        [Required, NotNull]
        [StringLength(25)]
        public required string ResidenceAddressHouse { get; set; }

        [Required, NotNull]
        [StringLength(20)]
        public required string ResidenceAddressPostalIndex { get; set; }
    }
}
