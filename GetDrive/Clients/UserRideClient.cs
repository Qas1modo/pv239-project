using GetDrive.Api;
using GetDrive.Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IUserRideClient
    {
        Task<ClientResponse<IEnumerable<PassengerDetailResponseDTO>>> GetDriverRequests();
        Task<ClientResponse<IEnumerable<PassengerDetailResponseDTO>>> GetPassengerRequests();
        Task<ClientResponse<string>> RequestRide(PassengerDTO requestRideDTO);
        Task<ClientResponse<string>> DeleteRequest(int id);
        Task<ClientResponse<string>> AcceptRide(int id);
    }

    public class UserRideClient : IUserRideClient
    {
        private readonly IGetDriveClient api;

        public UserRideClient(IGetDriveClient api)
        {
            this.api = api;
        }

        public async Task<ClientResponse<string>> AcceptRide(int id)
        {
            try
            {
                var response = await api.RequestsPUTAsync(id);;
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

        public async Task<ClientResponse<string>> DeleteRequest(int id)
        {
            try
            {
                var response = await api.RequestsDELETEAsync(id);
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

        public async Task<ClientResponse<IEnumerable<PassengerDetailResponseDTO>>> GetDriverRequests()
        {
            try
            {
                var response = await api.DriverAsync();
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ClientResponse<IEnumerable<PassengerDetailResponseDTO>>> GetPassengerRequests()
        {
            try
            {
                var response = await api.UserAllAsync();
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<IEnumerable<PassengerDetailResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ClientResponse<string>> RequestRide(PassengerDTO requestRideDTO)
        {
            try
            {
                var response = await api.RequestsPOSTAsync(requestRideDTO); ;
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
