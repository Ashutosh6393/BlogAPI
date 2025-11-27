using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MegaBlogAPI.Models;
using System;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Services.Implementation;
using MegaBlogAPI.Services.Interface;

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
        public Task<IActionResult> SignUp([FromBody] User user)
        {


            //return Ok("adsf");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {

            LoginResult result = await _authService.Login(loginDTO);

            if (result.Success) {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);    
            }
        }
    }
}
