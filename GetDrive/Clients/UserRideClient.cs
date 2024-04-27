using GetDrive.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IUserRideClient
    {
        Task<IEnumerable<PassengerDetailResponseDTO>> GetDriverRequests();
        Task<IEnumerable<PassengerDetailResponseDTO>> GetPassengerRequests();
        Task<string> RequestRide(PassengerDTO requestRideDTO);
        Task<string> DeleteRequest(int id);
        Task<string> AcceptRide(int id);
    }

    public class UserRideClient : IUserRideClient
    {
        private readonly Client api;

        public UserRideClient(Client api)
        {
            this.api = api;
        }

        public async Task<string> AcceptRide(int id)
        {
            return await api.RequestsPUTAsync(id);
        }

        public async Task<string> DeleteRequest(int id)
        {
            return await api.RequestsDELETEAsync(id);
        }

        public async Task<IEnumerable<PassengerDetailResponseDTO>> GetDriverRequests()
        {
            return await api.DriverAsync();
        }

        public async Task<IEnumerable<PassengerDetailResponseDTO>> GetPassengerRequests()
        {
            return await api.UserAllAsync();
        }

        public async Task<string> RequestRide(PassengerDTO requestRideDTO)
        {
            return await api.RequestsPOSTAsync(requestRideDTO);
        }
    }
}
