using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using System.Security.Claims;

namespace MegaBlogAPI.Services.Interface
{
    public interface IPostService
    {
        Task<MessageResponse> AddPost(PostInputDTO postInputDTO, ClaimsPrincipal User);
        Task<MessageResponse> DeletePost(int id);
        Task<MessageResponse> UpdatePost(UpdatePostDTO updatePostDTO);
        Task<PostListResponse> GetAllPosts();
        Task<PostResponse> GetPostById(int id);
    }
}
