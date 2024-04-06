using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    public class UserRideController : Controller
    {
        private readonly IUserRideService userRideService;

        public UserRideController(IUserRideService userRideService)
        {
            this.userRideService = userRideService;
        }

        [HttpGet("Driver")]
        [Authorize]
        public IActionResult GetDriverRequests(int requestUserId)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (requestUserId != userId)
            {
                return BadRequest("You are not allowed to get rides as this user");
            }
            var result = userRideService.GetDriverRequests(requestUserId);
            return Ok(result);
        }

        [HttpGet("User")]
        [Authorize]
        public IActionResult GetUserRequests(int requestUserId)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (requestUserId != userId)
            {
                return BadRequest("You are not allowed to get rides as this user");
            }
            var result = userRideService.GetUserRequests(requestUserId);
            return Ok(result);
        }

        [HttpPost("Join")]
        [Authorize]
        public async Task<IActionResult> JoinRide([FromBody] RequestRideDTO requestRideDTO)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (requestRideDTO.UserId != userId)
            {
                return BadRequest("You are not allowed to join rides as this user");
            }
            var result = await userRideService.JoinRide(requestRideDTO);
            if (!result)
            {
                return BadRequest("You cannot join this ride, its already full");
            }
            return Ok("Request to join ride has been sended");
        }

        [HttpPut("Accept")]
        [Authorize]
        public async Task<IActionResult> AcceptRide([FromBody] AcceptRideDTO acceptRideDTO)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (acceptRideDTO.UserId != userId)
            {
                return BadRequest("You are not allowed to accept rides as this user");
            }
            var result = await userRideService.AcceptRide(acceptRideDTO.UserRideId);
            if (!result)
            {
                return BadRequest("You cannot accept this request, ride is already full");
            }
            return Ok("Request to join ride has been sended");
        }
    }
}
