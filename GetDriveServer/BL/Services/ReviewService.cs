using AutoMapper;
using BL.DTOs;
using DAL.Models;
using DAL.UnitOfWork.Interface;

namespace BL.Services
{
    public interface IReviewService
    {
        Task<bool> AddReview(ReviewDTO reviewDTO, int authorId);
        Task<bool> DeleteReview(int reviewId, int authorId);
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

        public async Task<bool> AddReview(ReviewDTO reviewDTO, int authorId)
        {
            var doesReviewExist = uow.ReviewRepository.GetQueryable()
                .FirstOrDefault(review => review.UserId == reviewDTO.UserId && review.AuthorId == authorId);
            var doesUserExist = uow.UserRepository.GetQueryable()
                .FirstOrDefault(user => user.Id == reviewDTO.UserId);
            if (doesReviewExist != null || doesUserExist == null)
            {
                return false;
            }
            var review = mapper.Map<Review>(reviewDTO);
            review.PostedAt = DateTime.Now;
            review.AuthorId = authorId;
            uow.ReviewRepository.Insert(review);
            await uow.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteReview(int reviewId, int authorId)
        {
            var review = uow.ReviewRepository.GetQueryable()
                .FirstOrDefault(r => r.Id == reviewId && r.AuthorId == authorId);
            if (review == null)
            {
                return false;
            }
            uow.ReviewRepository.Delete(review);
            await uow.CommitAsync();
            return true;
        }
    }
}
