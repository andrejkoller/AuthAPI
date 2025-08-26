using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.DTOs;
using AuthAPI.Services;

namespace AuthAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequestDTO request)
        {
            var user = await authService.RegisterUserAsync(request);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequestDTO request)
        {
            var user = await authService.LoginUserAsync(request);
            return Ok(user);
        }
    }
}
