using System;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Services.Implementation;
using MegaBlogAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegaBlogAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpInputDTO signUpInputDTO)
        {
            AuthResponse result = await _authService.SignUp(signUpInputDTO);

            if (result.Success)
            {
                // attach jwt to cookie or send as response

                var token = _authService.GenerateJwt(result.AuthUserResponseDTO!.Email, result.AuthUserResponseDTO.Name);
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });


                return Ok(new { message = "Signup successful", token });
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputDto loginDTO)
        {
            AuthResponse result = await _authService.Login(loginDTO);

            if (result.Success)
            {
                var token = _authService.GenerateJwt(result.AuthUserResponseDTO!.Email, result.AuthUserResponseDTO.Name);
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Login successful", token });
            }

            return Unauthorized();

        }
    }
}
