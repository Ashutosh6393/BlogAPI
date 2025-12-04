using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository.Interface;
using MegaBlogAPI.Services;
using MegaBlogAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MegaBlogAPI.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserRepository _userRepository2;
        private readonly BlogDbContext _blogDbContext;
        private readonly IConfiguration _config;

        public AuthService(IRepository<User> userRepository, IUserRepository userRepository2, IConfiguration config, BlogDbContext blogDbContext)
        {
            _userRepository = userRepository;
            _userRepository2 = userRepository2;
            _blogDbContext = blogDbContext;
            _config = config;
        }

        public string GenerateJwt(string email, string name, int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("Name", name),
            new Claim("Email", email),
            new Claim("UserId", userId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<AuthResponse> SignOut()
        {
            throw new NotImplementedException();
        }

        async Task<AuthResponse> IAuthService.Login(LoginInputDto loginDto)
        {
            try
            {
                var userExists = await _userRepository2.GetByEmailAsync(loginDto.Email);
                if (userExists == null)
                {
                    return new AuthResponse(false, "User with this email doesn't exist", null);
                }

                if (!PasswordService.VerifyPassword(loginDto.Password, userExists.Password))
                {
                    return new AuthResponse(false, "Email or password incorrect", null);
                }

                var token = GenerateJwt(userExists.Email, userExists.Name, userExists.UserId);

                return new AuthResponse(true, "Login Successfull", token);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthResponse(false, "Some Error occured", null);

            }
        }

        async Task<AuthResponse> IAuthService.SignUp(SignUpInputDTO signUpDTO)
        {
            try
            {
                var userExists = await _userRepository2.GetByEmailAsync(signUpDTO.Email);
                if (userExists != null)
                {
                    return new AuthResponse(false, "User already exists", null);
                }

                string hashedPassword = PasswordService.HashPassword(signUpDTO.Password);

                User newUser = new User
                {
                    Email = signUpDTO.Email,
                    Name = signUpDTO.Name,
                    Password = hashedPassword,
                };

                var result = await _userRepository.AddAsync(newUser);

                var token = GenerateJwt(signUpDTO.Email, signUpDTO.Name, result.UserId);

                return new AuthResponse(
                    true,
                    "Signup successfull",
                    token
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthResponse(false, "Error signing up", null);
            }
        }



    }
}
