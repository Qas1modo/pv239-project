using BL.DTOs;
using BL.ResponseDTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<AuthResponseDTO> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid Data");
            }
            var data = authService.Login(loginDto);
            if (data == null)
            {
                return BadRequest("Invalid email or password");
            }
            return Ok(data);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponseDTO?>> Register([FromBody] RegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            var result = await authService.RegisterUserAsync(registrationDTO);
            if (result == null)
            {
                return BadRequest("User with this name or email already exists");
            }
            return Ok(result);
        }

        [HttpPost("changepassword")]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (!await authService.ChangePasswordAsync(changePasswordDTO, userId))
            {
                return BadRequest("Invalid old password");
            }
            return Ok("Password changed");
        }
    }
}
