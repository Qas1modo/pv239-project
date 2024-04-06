using AutoMapper;
using BL.DTOs;
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
        Task<bool> JoinRide(RequestRideDTO requestRideDTO);
        Task<bool> AcceptRide(int userRideId);
        IEnumerable<UserRide> GetDriverRequests(int userId);
        IEnumerable<UserRide> GetUserRequests(int userId);
    }
    public class UserRideService : IUserRideService
    {
        private readonly IUnitOfWork uow;

        public UserRideService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<bool> JoinRide(RequestRideDTO requestRideDTO)
        {
            var acceptedRides = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.RideId == requestRideDTO.RideId && ur.Accepted);
            var currentRide = await uow.RideRepository.GetByID(requestRideDTO.RideId);
            var currentPassangerCount = acceptedRides.Sum(s => s.PassengerCount);
            if ((currentPassangerCount + requestRideDTO.PassangerCount) > currentRide.MaxPassangerCount)
            {
                return false;
            }
            uow.UserRideRepository.Insert(new UserRide
            {
                RideId = requestRideDTO.RideId,
                PassengerId = requestRideDTO.UserId,
                PassengerCount = requestRideDTO.PassangerCount,
                PassangerNote = requestRideDTO.PassangerNote,
                Accepted = false
            });
            return true;
        }

        public async Task<bool> AcceptRide(int userRideId)
        {
            var userRide = await uow.UserRideRepository.GetByID(userRideId);
            if (userRide == null)
            {
                return false;
            }
            var acceptedRides = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.RideId == userRide.RideId && ur.Accepted);
            var currentPassangerCount = acceptedRides.Sum(s => s.PassengerCount);
            if ((currentPassangerCount + userRide.PassengerCount) > userRide.Ride.MaxPassangerCount)
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

        public IEnumerable<UserRide> GetDriverRequests(int userId)
        {
            var requests = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.Ride.DriverId == userId && !ur.Accepted && !ur.Ride.Canceled);
            return requests;
        }

        public IEnumerable<UserRide> GetUserRequests(int userId)
        {
            var requests = uow.UserRideRepository.GetQueryable()
                .Where(ur => ur.PassengerId == userId && !ur.Accepted && !ur.Ride.Canceled);
            return requests;
        }
    }
}
