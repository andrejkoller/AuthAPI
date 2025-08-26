using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Services;

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
    }
}
