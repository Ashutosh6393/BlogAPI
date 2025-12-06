using MegaBlogAPI.Models;

namespace MegaBlogAPI.DTO
{
    public record AuthServiceResponse(
        bool Success,
        string Message,
        string? Token
    );

    public record PostServiceResponse(bool Success, string Message, IEnumerable<AbstractPostDTO>? absPostDTO );
   
}
