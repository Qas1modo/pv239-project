using BL.DTOs;
using BL.ResponseDTOs;
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
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PassengerDetailResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PassengerDetailResponseDTO>> GetDriverRequests()
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            return Ok(userRideService.GetDriverRequests(userId));
        }

        [HttpGet("user")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PassengerDetailResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PassengerDetailResponseDTO>> GetUserRequests()
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            return Ok(userRideService.GetUserRequests(userId));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> RequestRide([FromBody] PassengerDTO requestRideDTO)
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
            var result = await userRideService.RequestRide(requestRideDTO, userId);
            if (!result)
            {
                return BadRequest(new ApiErrorResponseDTO("You cannot join this ride, it does not have necessary capacity or you are the owner"));
            }
            return Ok(new ApiSuccessResponseDTO("Request to join ride has been sent"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteRequest(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            var result = await userRideService.DeleteRequest(id, userId);
            if (!result)
            {
                return BadRequest(new ApiErrorResponseDTO("You cannot delete this ride"));
            }
            return Ok(new ApiSuccessResponseDTO("Request to join ride has been sent"));
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> AcceptRide(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int driverId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            var result = await userRideService.AcceptRide(id, driverId);
            if (!result)
            {
                return BadRequest(new ApiErrorResponseDTO(
                    "You cannot accept this request, ride is already full, you are not the owner or you already joined this ride"));
            }
            return Ok(new ApiSuccessResponseDTO("Request accepted"));
        }
    }
}
