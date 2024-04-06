using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Review> ReviewRepository { get; }
        IRepository<Ride> RideRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<UserRide> UserRideRepository { get; }
        void Commit();
        Task CommitAsync();
    }
}
