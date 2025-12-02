using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Services.Interface
{
    public interface IPostService
    {
        Task<MessageResponse> AddPost(PostInputDTO postInputDTO);
        Task<MessageResponse> DeletePost(int id);
        Task<MessageResponse> UpdatePost(UpdatePostDTO updatePostDTO);
        Task<PostListResponse> GetAllPosts();
        Task<PostResponse> GetPostById(int id);
    }
}
