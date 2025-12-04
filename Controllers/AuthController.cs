using System;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Services.Interface;
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
        public async Task<IActionResult> SignUp(SignUpInputDTO signUpInputDTO)
        {
            AuthResponse result = await _authService.SignUp(signUpInputDTO);

            if (result.Success && result.Token != null)
            {
                Response.Cookies.Append("jwt", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Signup successful", result.Token });
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputDto loginDTO)
        {
            AuthResponse result = await _authService.Login(loginDTO);

            if (result.Success)
            {
                Response.Cookies.Append("jwt", result.Token!, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Login successful", result.Token });
            }
            return Unauthorized(result);

        }

        [HttpPost("signout")]
        public IActionResult Signout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new AuthResponse(true, "Signout successfull", null));
        }
    }
}
