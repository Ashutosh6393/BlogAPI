using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginInputDto loginDto);
        Task<AuthResponse> SignUp(SignUpInputDTO signUpDTO);
        string GenerateJwt(string email, string name, int userId);

    }
}
