using AutoMapper;
using BL.DTOs;
using BL.ResponseDTOs;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;
using GetDrive.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IRideService
    {
        Task<Ride> CreateRide(CreateRideDTO rideDto, int driverId);
        IEnumerable<Ride> GetRides(RideFilterDTO filter);
        Task<RideDetailResponseDTO?> GetRide(int id);
        Task<bool> CancelRide(int id, int userId);
    }

    public class RideService : IRideService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IGeocodingService geocodingService;

        public RideService(IUnitOfWork uow, IMapper mapper, IGeocodingService geocodingService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.geocodingService = geocodingService;
        }

        public async Task<Ride> CreateRide(CreateRideDTO rideDto, int driverId)
        {
            var ride = mapper.Map<Ride>(rideDto);
            ride.Canceled = false;
            ride.DriverId = driverId;
            ride.AvailableSeats = rideDto.MaxPassengerCount;
            var startLocation = await geocodingService.GetLocationAsync(ride.StartLocation);
            ride.StartLongitude = startLocation?.Longitude ?? 0;
            ride.StartLatitude = startLocation?.Latitude ?? 0;
            var destination = await geocodingService.GetLocationAsync(ride.Destination);
            ride.DestinationLongitude = destination?.Longitude ?? 0;
            ride.DestinationLatitude = destination?.Latitude ?? 0;
            await uow.RideRepository.InsertAsync(ride);
            await uow.CommitAsync();
            return ride;
        }

        public IEnumerable<Ride> GetRides(RideFilterDTO filter)
        {
            var query = uow.RideRepository.GetQueryable();
            if (!(filter.ShowCanceled ?? false))
            {
                query = query.Where(r => !r.Canceled);
            }
            if (filter?.Departure != null)
            {
                query = query.Where(r => r.Departure.Date == filter.Departure);
            }
            if (filter?.StartLocation != null)
            {
                query = query.Where(r => r.StartLocation.Contains(filter.StartLocation));
            }
            if (filter?.Destination != null)
            {
                query = query.Where(r => r.Destination.Contains(filter.Destination));
            }
            if (filter?.AvailableSeats != null)
            {
                query = query.Where(r => r.AvailableSeats >= filter.AvailableSeats);
            }
            if (filter?.MaximumPrice != null)
            {
                query = query.Where(r => r.Price <= filter.MaximumPrice);
            }
            return query.ToList();
        }

        public async Task<RideDetailResponseDTO?> GetRide(int id)
        {
            var ride = await uow.RideRepository.GetByID(id);
            if (ride == null)
            {
                return null;
            }
            var rideDTO = mapper.Map<RideDetailResponseDTO>(ride);
            rideDTO.Passengers = mapper.Map<IEnumerable<PassengerResponseDTO>>(uow.UserRideRepository.GetQueryable().Where(ur => ur.RideId == id && ur.Accepted).ToList());
            rideDTO.DriverReviews = mapper.Map<IEnumerable<ReviewResponseDTO>>(uow.ReviewRepository.GetQueryable().Where(r => r.UserId == rideDTO.DriverId).ToList());
            return rideDTO;
        }

        public async Task<bool> CancelRide(int id, int userId)
        {
            var ride = await uow.RideRepository.GetByID(id);
            if (ride == null || userId != ride.DriverId || ride.Departure < DateTime.Now)
            {
                return false;
            }
            uow.UserRideRepository.GetQueryable().Where(ur => ur.RideId == id).ToList().ForEach(ur => uow.UserRideRepository.Delete(ur));
            if (!ride.Canceled)
            {
                ride.Canceled = true;
                uow.RideRepository.Update(ride);
                await uow.CommitAsync();
            }
            return true;
        }
    }
}
