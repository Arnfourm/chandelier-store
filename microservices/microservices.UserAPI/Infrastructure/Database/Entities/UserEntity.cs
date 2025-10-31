using microservices.UserAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, NotNull]
        [StringLength(250)]
        public required string Email { get; set; }

        [Required, NotNull]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required, NotNull]
        [StringLength(100)]
        public required string Surname { get; set; }

        [AllowNull]
        public DateOnly Birthday { get; set; }

        public DateTime Registration { get; set; }

        [Required, NotNull]
        public Guid PasswordId { get; set; }

        [ForeignKey("PasswordId")]
        public PasswordEntity? Password { get; set; }

        [AllowNull]
        public Guid RefreshTokenId { get; set; }

        [ForeignKey("RefreshTokenId")]
        public RefreshTokenEntity? RefreshToken { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public required UserRoleEnum UserRole { get; set; }
    }
}