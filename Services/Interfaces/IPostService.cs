using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ControllerInputDTO;
using System.Security.Claims;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Services.Interface
{
    public interface IPostService
    {
        Task<PostServiceResponse> AddPost(PostInputDTO postInputDTO);
        Task<PostServiceResponse> DeletePost(int id);
        Task<PostServiceResponse> UpdatePost(UpdatePostInputDTO updatePostDTO);
        Task<PostServiceResponse> GetAllPosts();
        Task<PostServiceResponse> GetPostById(int id);
        Task<PostServiceResponse> GetUserPosts(int id);
    }
}
