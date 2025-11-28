using MegaBlogAPI.Models;

namespace MegaBlogAPI.DTO.ReturnTypes
{
    public record AuthResponse(bool Success, string Message, User? user);


}
