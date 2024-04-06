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
        public async Task<IActionResult> GetRide(int id)
        {
            RideDetailResponseDTO? ride = await _rideService.GetRide(id);
            if (ride == null)
            {
                return NotFound("Ride not found");
            }
            return Ok(ride);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetRides([FromQuery] RideFilterDTO filter)
        {
            var rides = _rideService.GetRides(filter);
            var ridesDTO = _mapper.Map<IEnumerable<RideResponseDTO>>(rides);
            return Ok(ridesDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRide([FromBody] RideDTO rideDTO)
        {
            if (rideDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            if (!int.TryParse(User.Identity?.Name, out int driverId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (rideDTO.Departure < DateTime.Now.AddHours(2))
            {
                return BadRequest("Departure date must two hours from now");
            }
            var createdRide = await _rideService.CreateRide(rideDTO, driverId);
            if (createdRide == null)
            {
                return BadRequest("There was error when creating Ride");
            }
            return Ok(createdRide);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelRide(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            var result = await _rideService.CancelRide(id, userId);
            if (!result)
            {
                return NotFound("Ride can be deleted only if departure is in future");
            }
            return Ok("Successfully canceled");
        }
    }
}
