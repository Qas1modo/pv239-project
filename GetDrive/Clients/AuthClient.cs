using GetDrive.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IAuthClient
    {
        Task<(AuthResponseDTO? Data, string ErrorMessage)> Login(LoginDto loginDTO);
        Task<(AuthResponseDTO? Data, string ErrorMessage)> Register(RegistrationDTO registrationDTO);
        Task<bool> Logout();
        Task<string> ChangePassword(ChangePasswordDTO changePasswordDTO);
    }

    public class AuthClient : IAuthClient
    {
        private readonly IGetDriveClient _api;

        public AuthClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<(AuthResponseDTO? Data, string ErrorMessage)> Login(LoginDto loginDTO)
        {
            try
            {
                var response = await _api.LoginAsync(loginDTO);
                await SecureStorage.SetAsync("Token", response.Token);
                await SecureStorage.SetAsync("UserId", response.UserId.ToString());
                return (response, string.Empty);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == (int)System.Net.HttpStatusCode.BadRequest)
                {
                    return (null, "Invalid email or password"); 
                }
                return (null, ex.Message);
            }
            catch (Exception ex)
            {
                return (null, $"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<(AuthResponseDTO? Data, string ErrorMessage)> Register(RegistrationDTO registrationDTO)
        {
            try
            {
                var response = await _api.RegisterAsync(registrationDTO);
                if (response != null && response.Token != null)
                {
                    await SecureStorage.SetAsync("Token", response.Token);
                    await SecureStorage.SetAsync("UserId", response.UserId.ToString());
                    return (response, string.Empty);
                }
                return (null, "Registration failed."); 
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == (int)System.Net.HttpStatusCode.BadRequest)
                {
                    return (null, "User with this name/email already exists.");
                }
                return (null, ex.Message);
            }
            catch (Exception ex)
            {
                return (null, $"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<bool> Logout()
        {
            //Only delete the token from the client, do not invalidate the token on the server
            //TODO delete the token from the client
            SecureStorage.RemoveAll();
            return true;
        }

        public async Task<string> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {

            return await _api.ChangepasswordAsync(changePasswordDTO);
        }
    }
}
