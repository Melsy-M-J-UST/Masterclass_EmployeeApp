using Azure.Core;
using EmployeeApp.API.Dto;
using EmployeeApp.API.Services;
using EmployeeApp.API.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var (success, message, userId) = await service.Register(request);
            if (!success)
            {
                return BadRequest(new { message });
            }
            return Ok(new { message, userId });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var (Success, message, token, ExpiresIn) = await service.Login(request);
            if (!Success)
            {
                return Unauthorized(new {message});
            }
            AuthResponse response = new AuthResponse
            {
                accesstoken = token,
                message = message,
                ExpiresIn = ExpiresIn
            };
            return Ok(response);
        }
    }
}
