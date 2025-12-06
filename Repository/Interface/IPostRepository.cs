using MegaBlogAPI.DTO;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Repository.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> AddAsync(Post obj);
        Task UpdateAsync(Post obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Post>?> GetAllUserPosts(int userId);

    }
}
