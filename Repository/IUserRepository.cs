using MegaBlogAPI.Data;
using MegaBlogAPI.Models;


namespace MegaBlogAPI.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

    }
}
