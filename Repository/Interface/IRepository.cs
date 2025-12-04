using System;

namespace MegaBlogAPI.Repository.Interface
{
    public interface IRepository<T>
        where T : class
    {
        //CRUD
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);
    }
}
