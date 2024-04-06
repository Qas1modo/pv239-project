using AutoMapper;
using BL.ResponseDTOs;
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
        Task<UserProfileResponseDTO?> GetProfile(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow,
            IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<UserProfileResponseDTO?> GetProfile(int userId)
        {
            var user = await uow.UserRepository.GetByID(userId);
            if (user == null)
            {
                return null;
            }
            var ridesDriver = uow.RideRepository.GetQueryable().Where(ur => ur.DriverId == userId).ToList();
            var ridesPassenger = uow.UserRideRepository.GetQueryable().Where(ur => ur.PassengerId == userId && ur.Accepted).ToList();
            var userReviews = uow.ReviewRepository.GetQueryable().Where(r => r.UserId == userId).ToList();
            var overallScore = userReviews.Count == 0 ? 0 : ((decimal) userReviews.Sum(r => r.Score)) / userReviews.Count;
            return new UserProfileResponseDTO
            {
                UserName = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Score = overallScore,
                DriverRides = mapper.Map<IEnumerable<RideResponseDTO>>(ridesDriver),
                PassengerRides = mapper.Map<IEnumerable<PassengerDetailResponseDTO>>(ridesPassenger),
                Reviews = mapper.Map<IEnumerable<ReviewResponseDTO>>(userReviews)
            };
        }
    }
}
