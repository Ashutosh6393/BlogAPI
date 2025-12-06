using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO.ControllerInputDTO
{
    public class LoginInputDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
