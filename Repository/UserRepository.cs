using MegaBlogAPI.Data;
using MegaBlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaBlogAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(BlogDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();

        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
