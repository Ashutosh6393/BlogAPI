

using MegaBlogAPI.Models;

namespace MegaBlogAPI.Services.Interface
{
    interface IPostService
    {
        Task<Post> AddPost(Post post);
        Task<bool> DeletePost(int id);
        Task<bool> UpdatePost(Post post);
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById();

    }
}