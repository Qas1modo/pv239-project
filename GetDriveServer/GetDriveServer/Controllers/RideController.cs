using AutoMapper;
using BL.DTOs;
using BL.ResponseDTOs;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace GetDriveServer.Controllers
{
    [Route("ride")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;
        private readonly IMapper _mapper;

        public RideController(IRideService rideService, IMapper mapper)
        {
            _rideService = rideService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RideDetailResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<RideDetailResponseDTO>> GetRide(int id)
        {
            RideDetailResponseDTO? ride = await _rideService.GetRide(id);
            if (ride == null)
            {
                return NotFound(new ApiErrorResponseDTO("Ride not found"));
            }
            return Ok(ride);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<RideResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RideResponseDTO>> GetRides([FromQuery] RideFilterDTO filter)
        {
            var rides = _rideService.GetRides(filter);
            var ridesDTO = _mapper.Map<IEnumerable<RideResponseDTO>>(rides);
            return Ok(ridesDTO);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RideResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<RideResponseDTO>> CreateRide([FromBody] CreateRideDTO rideDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .Aggregate((a, b) => a + Environment.NewLine + b);
                return BadRequest(new ApiErrorResponseDTO(errorMessage));
            }
            if (!int.TryParse(User.Identity?.Name, out int driverId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            if (rideDTO.Departure < DateTime.Now.AddHours(2))
            {
                return BadRequest(new ApiErrorResponseDTO("Departure date must two hours from now"));
            }
            var createdRide = await _rideService.CreateRide(rideDTO, driverId);
            if (createdRide == null)
            {
                return BadRequest(new ApiErrorResponseDTO("There was error when creating Ride"));
            }
            var rideResponse = _mapper.Map<RideResponseDTO>(createdRide);
            return Ok(rideResponse);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> CancelRide(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            var result = await _rideService.CancelRide(id, userId);
            if (!result)
            {
                return NotFound(new ApiErrorResponseDTO("Ride can be deleted only if departure is in future"));
            }
            return Ok(new ApiSuccessResponseDTO("Successfully cancelled"));
        }
    }
}
