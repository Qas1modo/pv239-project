using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    [Route("requests")]
    public class UserRideController : Controller
    {
        private readonly IUserRideService userRideService;

        public UserRideController(IUserRideService userRideService)
        {
            this.userRideService = userRideService;
        }

        [HttpGet("driver")]
        [Authorize]
        public IActionResult GetDriverRequests()
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            return Ok(userRideService.GetDriverRequests(userId));
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUserRequests()
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            return Ok(userRideService.GetUserRequests(userId));
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> JoinRide([FromBody] PassengerDTO requestRideDTO)
        {
            if (requestRideDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            var result = await userRideService.JoinRide(requestRideDTO, userId);
            if (!result)
            {
                return BadRequest("You cannot join this ride, it does not have necessary capacity or you are the owner");
            }
            return Ok("Request to join ride has been sended");
        }

        [HttpPut("accept/{id}")]
        [Authorize]
        public async Task<IActionResult> AcceptRide(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int driverId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            var result = await userRideService.AcceptRide(id, driverId);
            if (!result)
            {
                return BadRequest("You cannot accept this request, ride is already full, you are not the owner or you already joined this ride");
            }
            return Ok("Request accepted");
        }
    }
}
