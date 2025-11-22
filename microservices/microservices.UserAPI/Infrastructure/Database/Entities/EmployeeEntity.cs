using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("Employee")]
    public class EmployeeEntity
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity? User { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [StringLength(125)]
        public required string ResidenceAddressCountry { get; set; }

        [Required]
        [StringLength(150)]
        public required string ResidenceAddressDistrict { get; set; }

        [Required]
        [StringLength(150)]
        public required string ResidenceAddressCity { get; set; }

        [Required]
        [StringLength(200)]
        public required string ResidenceAddressStreet { get; set; }

        [Required]
        [StringLength(25)]
        public required string ResidenceAddressHouse { get; set; }

        [Required]
        [StringLength(20)]
        public required string ResidenceAddressPostalIndex { get; set; }
    }
}
