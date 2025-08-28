using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Services;
using AuthAPI.DTOs;

namespace AuthAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{userId}/name")]
        public async Task<IActionResult> UpdateUserNameAsync(int userId, [FromBody] UpdateNameRequestDTO request)
        {
            var updatedUser = await userService.UpdateUserName(userId, request);

            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }

        [HttpPut("{userId}/email")]
        public async Task<IActionResult> UpdateUserEmailAsync(int userId, [FromBody] UpdateEmailRequestDTO request)
        {
            var updatedUser = await userService.UpdateUserEmail(userId, request);

            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }
    }
}
