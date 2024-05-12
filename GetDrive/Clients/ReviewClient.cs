using GetDrive.Api;
using GetDrive.Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IReviewClient
    {
        Task<ClientResponse<string>> CreateReviewAsync(ReviewDTO review);
        Task<ClientResponse<string>> DeleteReviewAsync(int id);
    }

    public class ReviewClient : IReviewClient
    {

        private readonly IGetDriveClient _api;
        public ReviewClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<ClientResponse<string>> CreateReviewAsync(ReviewDTO review)
        {
            try
            {
                var response = await _api.ReviewPOSTAsync(review);
                return new ClientResponse<string>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<string>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<string>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ClientResponse<string>> DeleteReviewAsync(int id)
        {
            try
            {
                var response = await _api.ReviewDELETEAsync(id);
                return new ClientResponse<string>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<string>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<string>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
