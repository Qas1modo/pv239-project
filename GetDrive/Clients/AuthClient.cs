using GetDrive.Api;
using GetDrive.Clients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IAuthClient
    {
        Task<ClientResponse<AuthResponseDTO>> Login(LoginDto loginDTO);
        Task<ClientResponse<AuthResponseDTO>> Register(RegistrationDTO registrationDTO);
        Task<bool> Logout();
        Task<ClientResponse<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
    }

    public class AuthClient : IAuthClient
    {
        private readonly IGetDriveClient _api;

        public AuthClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<ClientResponse<AuthResponseDTO>> Login(LoginDto loginDTO)
        {
            try
            {
                var response = await _api.LoginAsync(loginDTO);
                await SecureStorage.SetAsync("Token", response.Token);
                await SecureStorage.SetAsync("UserId", response.UserId.ToString());
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException<ApiErrorResponseDTO> ex)
            {
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = null,
                    ErrorMessage = ex.Result.ErrorMessage,
                    StatusCode = ex.StatusCode
                }; 
            }
            catch (Exception ex)
            {
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ClientResponse<AuthResponseDTO>> Register(RegistrationDTO registrationDTO)
        {
            try
            {
                var response = await _api.RegisterAsync(registrationDTO);
                await SecureStorage.SetAsync("Token", response.Token);
                await SecureStorage.SetAsync("UserId", response.UserId.ToString());
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException<ApiErrorResponseDTO> ex)
            {
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = null,
                    ErrorMessage = ex.Result.ErrorMessage,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<AuthResponseDTO>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<bool> Logout()
        {
            SecureStorage.RemoveAll();
            return true;
        }

        public async Task<ClientResponse<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var response = await _api.ChangepasswordAsync(changePasswordDTO);
                return new ClientResponse<string>
                {
                    Response = response.Message,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException<ApiErrorResponseDTO> ex)
            {
                return new ClientResponse<string>
                {
                    Response = null,
                    ErrorMessage = ex.Result.ErrorMessage,
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
