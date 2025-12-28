using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microservices.ReviewAPI.Infrastructure.Database.Entities
{
    [Table("Review")]
    public class ReviewEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(1)]
        public int Rate { get; set; }

        [StringLength(300)]
        public string? Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
