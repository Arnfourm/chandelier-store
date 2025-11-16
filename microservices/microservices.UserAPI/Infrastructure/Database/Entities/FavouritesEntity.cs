using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.UserAPI.Infrastructure.Database.Entities
{
    [Table("Favorites")]
    public class FavoritesEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity? User { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}