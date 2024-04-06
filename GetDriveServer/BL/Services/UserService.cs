using AutoMapper;
using BL.DTOs;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetProfile(int userId);
    }
    public class UserService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow,
            IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<UserProfileDTO> GetProfile(int userId)
        {
            var user = await uow.UserRepository.GetByID(userId);
            var userRidesDriver = uow.UserRideRepository.GetQueryable().Where(ur => ur.Ride.DriverId == userId).ToList();
            var userRidesPassanger = uow.UserRideRepository.GetQueryable().Where(ur => ur.Ride.DriverId == userId).ToList();
            var userReviews = uow.ReviewRepository.GetQueryable().Where(r => r.UserId == userId).ToList();
            var overallScore = userReviews.Count == 0 ? 0 : ((decimal) userReviews.Sum(r => r.Score)) / userReviews.Count;
            return new UserProfileDTO
            {
                UserName = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Score = overallScore,
                DriverRides = mapper.Map<IEnumerable<RideOutDTO>>(userRidesDriver),
                PassangerRides = mapper.Map<IEnumerable<RideOutDTO>>(userRidesPassanger),
                Reviews = mapper.Map<IEnumerable<ReviewOutDTO>>(userReviews)
            };
        }
    }
}
