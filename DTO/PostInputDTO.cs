


using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO
{

    public class PostInputDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        [MinLength(100)]
        public required string Content { get; set; }

        [Required]
        public required int UserId { get; set; }
    }
}