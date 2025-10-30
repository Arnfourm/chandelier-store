using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("Password")]
    public class PasswordEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        [Required]
        [Column(TypeName = "bytea")]
        public required byte[] PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "bytea")]
        public required byte[] PasswordSaulHash { get; set; }
    }
}
