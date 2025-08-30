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

        [HttpDelete("{userId}/deactivate")]
        public async Task<IActionResult> DeactivateUserAsync(int userId)
        {
            var success = await userService.DeactivateUserAsync(userId);

            if (!success)
                return NotFound("User not found.");

            return Ok(new { Message = "User deactivated successfully." });
        }

        [HttpPost("{userId}/reactivate")]
        public async Task<IActionResult> ReactivateUserAsync(int userId)
        {
            var success = await userService.ReactivateUserAsync(userId);

            if (!success)
                return NotFound("User not found.");

            return Ok(new { Message = "User reactivated successfully." });
        }

        [HttpDelete("{userId}/delete")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var success = await userService.DeleteUserAsync(userId);

            if (!success)
                return NotFound("User not found.");

            return Ok(new { Message = "User deleted successfully." });
        }

        [HttpPost("{userId}/subscribe")]
        public async Task<IActionResult> SubscribeNewsletterAsync(int userId, [FromBody] UpdateNewsletterSubscribeRequestDTO request)
        {
            var updatedUser = await userService.SubscribeNewsletterAsync(userId, request);

            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }
    }
}
