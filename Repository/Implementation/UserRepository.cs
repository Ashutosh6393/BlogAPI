using MegaBlogAPI.Data;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MegaBlogAPI.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(BlogDbContext context)
        {
            _dbSet = context.Set<User>();

        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
