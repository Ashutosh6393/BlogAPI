using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository;
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

        public string GenerateJwt(string email, string name)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(JwtRegisteredClaimNames.Email, email)
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

        async Task<AuthResponse> IAuthService.Login(LoginInputDto loginDto)
        {
            try
            {
                User userExists = await _userRepository2.GetByEmailAsync(loginDto.Email);
                if (userExists == null)
                {
                    return new AuthResponse(false, "User with this email doesn't exist", null);
                }

                if (!PasswordService.VerifyPassword(loginDto.Password, userExists.Password))
                {
                    return new AuthResponse(false, "Email or password incorrect", null);
                }


                return new AuthResponse(true, "Login Successfull", new AuthUserResponseDTO { Email = userExists.Email, Name = userExists.Name });

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

                await _userRepository.AddAsync(newUser);
                await _blogDbContext.SaveChangesAsync();

                //TODO: sign new jwt and sent to attach to dto

                return new AuthResponse(
                    true,
                    "Signup successfull",
                    new AuthUserResponseDTO { Email = newUser.Email, Name = newUser.Name }
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
