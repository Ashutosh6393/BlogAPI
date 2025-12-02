using MegaBlogAPI.Models;

namespace MegaBlogAPI.DTO.ReturnTypes
{
    public record AuthResponse(
        bool Success,
        string Message,
        AuthUserResponseDTO? AuthUserResponseDTO
    );

    public record MessageResponse(bool Success, string Message);

    public record PostResponse(bool Success, string Message, Post? post);
    public record PostListResponse(bool Success, string Message, IEnumerable<Post>? posts);
}
