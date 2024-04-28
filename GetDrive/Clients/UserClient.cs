using GetDrive.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IUserClient
    {
        Task<UserProfileResponseDTO> GetProfile(int id);
    }
    public class UserClient: IUserClient
    {
        private readonly IGetDriveClient _api;

        public UserClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<UserProfileResponseDTO> GetProfile(int id)
        {
            return await _api.UserAsync(id);
        }
    }
}
