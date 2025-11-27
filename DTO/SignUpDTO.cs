

using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO

{
    public class SignUpDTO
    {
        [Required]
        [MinLength(2)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        
    }
}