using GetDrive.Api;
using GetDrive.Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IUserClient
    {
        Task<ClientResponse<UserProfileResponseDTO>> GetProfile(int id);
    }
    public class UserClient: IUserClient
    {
        private readonly IGetDriveClient _api;

        public UserClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<ClientResponse<UserProfileResponseDTO>> GetProfile(int id)
        {
            try
            {
                var response = await _api.UserAsync(id);
                return new ClientResponse<UserProfileResponseDTO>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<UserProfileResponseDTO>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<UserProfileResponseDTO>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
