using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;

namespace DAL.UnitOfWork
{
    public class UoWReview : IUoWReview
    {
        public IRepository<Review> ReviewRepository { get; }

        private readonly GetDriveDbContext context;

        public UoWReview(GetDriveDbContext context,
            IRepository<Review> reviewRepository)
        {
            this.context = context;
            ReviewRepository = reviewRepository;
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
