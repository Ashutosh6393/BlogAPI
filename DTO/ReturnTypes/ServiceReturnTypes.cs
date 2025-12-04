using MegaBlogAPI.Models;

namespace MegaBlogAPI.DTO.ReturnTypes
{
    public record AuthResponse(
        bool Success,
        string Message,
        string? Token
    );

    public record MessageResponse(bool Success, string Message);

    public record PostResponse(bool Success, string Message, PostResponseDTO? post);
    public record PostListResponse(bool Success, string Message, IEnumerable<Post>? posts);
}
