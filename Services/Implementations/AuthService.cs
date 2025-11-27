using MegaBlogAPI.DTO;
using MegaBlogAPI.Models;
using MegaBlogAPI.Services.Interface;
using MegaBlogAPI.Repository;
using MegaBlogAPI.Data;
using Microsoft.EntityFrameworkCore;
using MegaBlogAPI.DTO.ReturnTypes;

namespace MegaBlogAPI.Services.Implementation
{

   
    class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly BlogDbContext _blogDbContext;
        public AuthService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }

        async Task<LoginResult> IAuthService.Login(LoginDto loginDto)
        {
            var userExists = _blogDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (userExists == null)
            {
                return new LoginResult(false, "User with this email doesn't exist", null);
            }

            var authenticatedUser = await _blogDbContext.Users.FirstOrDefaultAsync(u => u.Password == loginDto.Password);


            if(authenticatedUser == null)
            {
                return new LoginResult(false, "Email or password incorrect", null);
            }


            return new LoginResult(true, "Login Successfull", authenticatedUser);

        }

        Task<User> IAuthService.SignUp(SignUpDTO signUpDTO)
        {
            var userExists = _blogDbContext.Users.FirstOrDefaultAsync(u => signUpDTO.Email == u.Email);

            if (userExists != null)
            {

            }
            

        }
    }

}