using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Review> ReviewRepository { get; }
        public IRepository<Ride> RideRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<UserRide> UserRideRepository { get; }

        private readonly GetDriveDbContext context;

        public UnitOfWork(GetDriveDbContext context,
            IRepository<Review> reviewRepository,
            IRepository<Ride> rideRepository,
            IRepository<User> userRepository,
            IRepository<UserRide> userRideRepository)
        {
            this.context = context;
            ReviewRepository = reviewRepository;
            RideRepository = rideRepository;
            UserRepository = userRepository;
            UserRideRepository = userRideRepository;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
