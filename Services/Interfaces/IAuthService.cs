using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ControllerInputDTO;
using MegaBlogAPI.Models;

namespace MegaBlogAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthServiceResponse> Login(LoginInputDTO loginDto);
        Task<AuthServiceResponse> SignUp(SignupInputDTO signUpDTO);
        string GenerateJwt(string email, string name, int userId);
    }
}
