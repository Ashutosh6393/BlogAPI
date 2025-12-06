using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO.ControllerInputDTO
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

    }
}