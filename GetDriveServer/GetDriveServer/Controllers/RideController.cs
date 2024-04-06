using AutoMapper;
using BL.DTOs;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace GetDriveServer.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> GetRide(int id)
        {
            var ride = await _rideService.GetRide(id);
            if (ride == null)
            {
                return NotFound("Ride not found");
            }
            return Ok(ride);
        }

        [HttpGet("Rides")]
        public IActionResult GetRides([FromBody] RideFilterDTO filter)
        {
            var rides = _rideService.GetRides(filter);
            var ridesDTO = _mapper.Map<IEnumerable<RideDTO>>(rides);
            return Ok(ridesDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRide([FromBody] RideDTO rideDTO)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (rideDTO.DriverId != userId)
            {
                return BadRequest("You are not allowed to asiign rides as current user");
            }
            if (rideDTO.Departure < DateTime.Now.AddHours(2))
            {
                return BadRequest("Departure date must two hours from now");
            }
            var createdRide = await _rideService.CreateRide(rideDTO);
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
                return NotFound("Ride can be deleted only if departure is in past");
            }
            return Ok("Successfully canceled");
        }
    }
}
