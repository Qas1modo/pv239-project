using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await userService.GetProfile(id);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }
            return Ok(profile);
        }
    }
}
