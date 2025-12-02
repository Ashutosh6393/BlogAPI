using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO
{
    public class AuthUserResponseDTO
    {
        public required string Email { get; set; }
        public required string Name { get; set; }

        // jwt when implemented
    }
}
