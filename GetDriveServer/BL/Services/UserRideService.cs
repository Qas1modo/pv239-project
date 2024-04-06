using AutoMapper;
using BL.DTOs;
using BL.ResponseDTOs;
using DAL.Models;
using DAL.UnitOfWork.Interface;
using Microsoft.AspNetCore.Connections.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{

    public interface IUserRideService
    {
        Task<bool> JoinRide(PassengerDTO requestRideDTO, int userId);
        Task<bool> AcceptRide(int userRideId, int driverId);
        IEnumerable<PassengerDetailResponseDTO> GetDriverRequests(int userId);
        IEnumerable<PassengerDetailResponseDTO> GetUserRequests(int userId);
    }
    public class UserRideService : IUserRideService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserRideService(IUnitOfWork uow,
            IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<bool> JoinRide(PassengerDTO requestRideDTO, int userId)
        {
            var allRides = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.RideId == requestRideDTO.RideId);
            var currentRide = await uow.RideRepository.GetByID(requestRideDTO.RideId);
            if (currentRide == null ||
                currentRide.DriverId == userId ||
                allRides.Any(r => r.PassengerId == userId))
            {
                return false;
            }
            var currentPassengerCount = allRides.Where(a => a.Accepted).Sum(s => s.PassengerCount);
            if ((currentPassengerCount + requestRideDTO.PassengerCount) > currentRide.MaxPassengerCount)
            {
                return false;
            }
            uow.UserRideRepository.Insert(new UserRide
            {
                RideId = requestRideDTO.RideId,
                PassengerId = userId,
                PassengerCount = requestRideDTO.PassengerCount,
                PassengerNote = requestRideDTO.PassengerNote,
                Accepted = false
            });
            await uow.CommitAsync();
            return true;
        }

        public async Task<bool> AcceptRide(int userRideId, int driverId)
        {
            var userRide = await uow.UserRideRepository.GetByID(userRideId);
            if (userRide == null || userRide.Ride.DriverId != driverId)
            {
                return false;
            }
            var acceptedRides = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.RideId == userRide.RideId && ur.Accepted);
            var currentPassengerCount = acceptedRides.Sum(s => s.PassengerCount);
            if ((currentPassengerCount + userRide.PassengerCount) > userRide.Ride.MaxPassengerCount)
            {
                return false;
            }
            userRide.Accepted = true;
            var ride = userRide.Ride;
            ride.AvailableSeats -= userRide.PassengerCount;
            uow.UserRideRepository.Update(userRide);
            uow.RideRepository.Update(ride);
            await uow.CommitAsync();
            return true;
        }

        public IEnumerable<PassengerDetailResponseDTO> GetDriverRequests(int userId)
        {
            var requests = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.Ride.DriverId == userId && !ur.Accepted && !ur.Ride.Canceled &&
                ur.Ride.AvailableSeats >= ur.PassengerCount);
            return mapper.Map<IEnumerable<PassengerDetailResponseDTO>>(requests);
        }

        public IEnumerable<PassengerDetailResponseDTO> GetUserRequests(int userId)
        {
            var requests = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.PassengerId == userId && !ur.Accepted && !ur.Ride.Canceled);
            return mapper.Map<IEnumerable<PassengerDetailResponseDTO>>(requests);
        }
    }
}
