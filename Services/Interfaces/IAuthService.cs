
using MegaBlogAPI.DTO;
using MegaBlogAPI.Models;
using MegaBlogAPI.DTO.ReturnTypes;

namespace MegaBlogAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginDto loginDto);
        Task<User> SignUp(SignUpDTO signUpDTO);
    }
}