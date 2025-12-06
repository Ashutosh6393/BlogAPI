using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MegaBlogAPI.Repository.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext dbContext;
        public PostRepository(BlogDbContext context)
        {
            dbContext = context;
        }
        public async Task<Post> AddAsync(Post obj)
        {
            var entity = await dbContext.Posts.AddAsync(obj);
            await dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task DeleteAsync(int id)
        {
            var res = await dbContext.Posts.FindAsync(id);
            if (res != null)
            {
                dbContext.Posts.Remove(res);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await dbContext.Posts.ToListAsync();
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            //return await dbContext.Posts.FindAsync(id);

            return await dbContext.Posts
                        .Include(p => p.User)
                        .Include(p => p.Comment)
                        .FirstOrDefaultAsync(p => p.PostId == id);
        }
        public async Task UpdateAsync(Post obj)
        {
            dbContext.Posts.Update(obj);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Post>?> GetAllUserPosts(int userId)
        {
            return await dbContext.Posts.Include(p => p.UserId == userId).ToListAsync();
        }
    }
}
