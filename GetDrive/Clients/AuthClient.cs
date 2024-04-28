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
        Task<AuthResponseDTO> Login(LoginDto loginDTO);
        Task<AuthResponseDTO> Register(RegistrationDTO registrationDTO);
        Task<bool> Logout();
        Task<string> ChangePassword(ChangePasswordDTO changePasswordDTO);
    }

    public class AuthClient : IAuthClient
    {
        private readonly Client _api;

        public AuthClient(Client api)
        {
            _api = api;
        }

        public async Task<AuthResponseDTO> Login(LoginDto loginDTO)
        {
            return await _api.LoginAsync(loginDTO);
        }

        public async Task<AuthResponseDTO> Register(RegistrationDTO registrationDTO)
        {
            var response = await _api.RegisterAsync(registrationDTO);
            await SecureStorage.SetAsync("Token", response.Token);
            return response;
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
