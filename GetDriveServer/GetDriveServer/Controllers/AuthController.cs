using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var data = authService.Login(loginDto);
            if (data == null)
            {
                return BadRequest("Invalid email or password");
            }
            return Ok(data);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
        {
            var result = await authService.RegisterUserAsync(registrationDTO);
            if (result == null)
            {
                return BadRequest("User with this name or email already exists");
            }
            return Ok(result);
        }

        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (changePasswordDTO.UserId != userId)
            {
                return BadRequest("You are not allowed to change this password");
            }
            if (!await authService.ChangePasswordAsync(changePasswordDTO))
            {
                return BadRequest("Invalid old password");
            }
            return Ok("Password changed");
        }
    }
}
