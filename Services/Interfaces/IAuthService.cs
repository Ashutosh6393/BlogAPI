
using MegaBlogAPI.DTO;
using MegaBlogAPI.Models;
using MegaBlogAPI.DTO.ReturnTypes;

namespace MegaBlogAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto loginDto);
        Task<AuthResponse> SignUp(SignUpDTO signUpDTO);
    }
}