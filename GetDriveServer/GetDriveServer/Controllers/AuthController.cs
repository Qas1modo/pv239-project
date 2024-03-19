using BL.DTOs;
using BL.Services.HoldingService;
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

        [HttpPost(Name = "Login")]
        public JsonResult Login([FromBody] LoginDto loginDto)
        {
            var data = authService.Login(loginDto);
            return new JsonResult(data);
        }
    }
}
