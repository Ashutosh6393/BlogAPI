using MegaBlogAPI.Data;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
