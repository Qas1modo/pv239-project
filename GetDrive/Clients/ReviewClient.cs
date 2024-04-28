using GetDrive.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IReviewClient
    {
        Task<string> CreateReviewAsync(ReviewDTO review);
        Task<string> DeleteReviewAsync(int id);
    }

    public class ReviewClient : IReviewClient
    {

        private readonly IGetDriveClient _api;
        public ReviewClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<string> CreateReviewAsync(ReviewDTO review)
        {
            return await _api.ReviewPOSTAsync(review);
        }

        public async Task<string> DeleteReviewAsync(int id)
        {
            return await _api.ReviewDELETEAsync(id);
        }
    }
}
