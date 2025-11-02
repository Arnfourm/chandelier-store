using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("RefreshToken")]
    public class RefreshTokenEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, NotNull]
        public required string Token { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime ExpireTime {  get; set; }
    }
}
