using DAL.Models;
using DAL.Repository;

namespace DAL.UnitOfWork.Interface
{
    public interface IUoWReview : IUnitOfWork
    {
        IRepository<Review> ReviewRepository { get; }
    }
}
