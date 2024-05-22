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
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<AuthResponseDTO> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .Aggregate((a, b) => a + Environment.NewLine + b);
                return BadRequest(new ApiErrorResponseDTO(errorMessage));
            }
            var data = authService.Login(loginDto);
            if (data == null)
            {
                return BadRequest(new ApiErrorResponseDTO("Invalid username or password"));
            }
            return Ok(data);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponseDTO?>> Register([FromBody] RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .Aggregate((a, b) => a + "\n" + b);
                return BadRequest(new ApiErrorResponseDTO(errorMessage));
            }
            var result = await authService.RegisterUserAsync(registrationDTO);
            if (result == null)
            {
                return BadRequest(new ApiErrorResponseDTO("User with this name or email already exists"));
            }
            return Ok(result);
        }

        [HttpPost("changepassword")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .Aggregate((a, b) => a + Environment.NewLine + b);
                return BadRequest(new ApiErrorResponseDTO(errorMessage));
            }
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            if (!await authService.ChangePasswordAsync(changePasswordDTO, userId))
            {
                return BadRequest(new ApiErrorResponseDTO("Invalid old password"));
            }
            return Ok(new ApiSuccessResponseDTO("Password changed"));
        }
    }
}
