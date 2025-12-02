using System;

namespace MegaBlogAPI.Repository
{
    public interface IRepository<T>
        where T : class
    {
        //CRUD
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);
    }
}
