using MegaBlogAPI.Models;

namespace MegaBlogAPI.DTO.ReturnTypes
{
    public record LoginResult(bool Success, string Message, User? user);


}
