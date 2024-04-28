using GetDrive.Api;
using GetDrive.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients
{
    public interface IRideClient
    {
        Task<RideDetailResponseDTO> GetRide(int id);
        Task<RideResponseDTO> CreateRide(CreateRideDTO rideCreateDTO);
        Task<string> CancelRide(int id);
        Task<IEnumerable<RideResponseDTO>> GetAllRides(RideFilterDTO filter);
    }

    public class RideClient : IRideClient
    {
        private readonly Client _api;

        public RideClient(Client api)
        {
            _api = api;
        }

        public async Task<RideDetailResponseDTO> GetRide(int id)
        {
            return await _api.RideGETAsync(id);
        }

        public Task<RideResponseDTO> CreateRide(CreateRideDTO rideCreateDTO)
        {
            return _api.RidePOSTAsync(rideCreateDTO);
        }

        public async Task<string> CancelRide(int id)
        {
            return await _api.RideDELETEAsync(id);
        }

        public async Task<IEnumerable<RideResponseDTO>> GetAllRides(RideFilterDTO filter)
        {
            return await _api.RideAllAsync(filter.StartLocation, filter.Destination, filter.Departure, (double?) filter.MaximumPrice,
                filter.AvailableSeats, filter.ShowCanceled);
        }
    }
}
