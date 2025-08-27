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

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var authHeader = HttpContext.Request.Headers.Authorization.FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Missing or invalid Authorization header.");
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var user = await authService.GetCurrentUserByToken(token);

            return Ok(user);
        }
    }
}
