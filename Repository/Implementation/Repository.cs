using MegaBlogAPI.Data;
using MegaBlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MegaBlogAPI.Repository.Implementation
{
    class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(BlogDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T obj)
        {
            var entity =  await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _dbSet.FindAsync(id);
            if (res != null)
            {
                _dbSet.Remove(res);
                await _context.SaveChangesAsync();
            }
        }
    }
}
