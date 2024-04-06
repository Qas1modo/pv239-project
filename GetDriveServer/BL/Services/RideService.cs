using AutoMapper;
using BL.DTOs;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IRideService
    {
        Task<Ride> CreateRide(RideDTO rideDto);
        IEnumerable<Ride> GetRides(RideFilterDTO filter);
        Task<RideDetailDTO> GetRide(int id);
        Task<bool> CancelRide(int id, int userId);
    }

    public class RideService : IRideService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public RideService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Ride> CreateRide(RideDTO rideDto)
        {
            var ride = mapper.Map<Ride>(rideDto);
            ride.Canceled = false;
            ride.AvailableSeats = rideDto.MaxPassangerCount;
            await uow.RideRepository.InsertAsync(ride);
            await uow.CommitAsync();
            return ride;
        }

        public IEnumerable<Ride> GetRides(RideFilterDTO filter)
        {
            var query = uow.RideRepository.GetQueryable();
            if (filter?.Date != null)
            {
                query.Where(r => r.Departure.Date == filter.Date);
            }
            if (filter?.StartLocation != null)
            {
                query.Where(r => r.StartLocation.Contains(filter.StartLocation));
            }
            if (filter?.Destination != null)
            {
                query.Where(r => r.Destination.Contains(filter.Destination));
            }
            if (filter?.AvailableSeats != null)
            {
                query.Where(r => r.AvailableSeats >= filter.AvailableSeats);
            }
            if (filter?.MaximumPrice != null)
            {
                query.Where(r => r.Price <= filter.MaximumPrice);
            }
            return query.ToList();
        }

        public async Task<RideDetailDTO> GetRide(int id)
        {
            var ride = mapper.Map<RideDetailDTO>(await uow.RideRepository.GetByID(id));
            ride.Passangers = mapper.Map<IEnumerable<PassangerDTO>>(uow.UserRideRepository.GetQueryable().Where(ur => ur.RideId == id).ToList());
            ride.Reviews = mapper.Map<IEnumerable<ReviewOutDTO>>(uow.ReviewRepository.GetQueryable().Where(r => r.RideId == id).ToList());
            return ride;
        }

        public async Task<bool> CancelRide(int id, int userId)
        {
            var ride = await uow.RideRepository.GetByID(id);
            if (ride == null || userId != ride.DriverId || ride.Departure < DateTime.Now)
            {
                return false;
            }
            uow.UserRideRepository.GetQueryable().Where(ur => ur.RideId == id).ToList().ForEach(ur => uow.UserRideRepository.Delete(ur));
            ride.Canceled = true;
            uow.RideRepository.Update(ride);
            await uow.CommitAsync();
            return true;
        }
    }
}
