using AutoMapper;
using BL.DTOs;
using DAL.Models;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IReviewService
    {
        Task<bool> AddReview(ReviewDTO reviewDTO);
    }

    public class ReviewService : IReviewService 
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ReviewService(IUnitOfWork uow,
            IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> AddReview(ReviewDTO reviewDTO)
        {
            var reviewedUserRide = uow.UserRideRepository.GetQueryable()
                .Where(r => r.RideId == reviewDTO.RideId && r.PassengerId == reviewDTO.AuthorId).FirstOrDefault();
            if (reviewedUserRide is null)
            {
                return false;
            }
            var review = mapper.Map<Review>(reviewDTO);
            review.UserId = reviewedUserRide.Ride.DriverId;
            var curentTime = DateTime.Now;
            if (reviewedUserRide.Ride.Departure > curentTime)
            {
                return false;
            }
            review.PostedAt = curentTime;
            uow.ReviewRepository.Insert(review);
            return true;
        }
    }
}
