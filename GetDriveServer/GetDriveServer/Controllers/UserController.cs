using BL.ResponseDTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserProfileResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileResponseDTO>> GetProfile(int id)
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
