using GetDrive.Api;
using GetDrive.Clients.Model;
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
        Task<ClientResponse<RideDetailResponseDTO>> GetRide(int id);
        Task<ClientResponse<RideResponseDTO>> CreateRide(CreateRideDTO rideCreateDTO);
        Task<string> CancelRide(int id);
        Task<ClientResponse<IEnumerable<RideResponseDTO>>> GetAllRides(RideFilterDTO filter);
    }

    public class RideClient : IRideClient
    {
        private readonly IGetDriveClient _api;

        public RideClient(IGetDriveClient api)
        {
            _api = api;
        }

        public async Task<ClientResponse<RideDetailResponseDTO>> GetRide(int id)
        {
            try
            {
                var response = await _api.RideGETAsync(id);
                return new ClientResponse<RideDetailResponseDTO>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<RideDetailResponseDTO>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<RideDetailResponseDTO>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }        
        }

        public async Task<ClientResponse<RideResponseDTO>> CreateRide(CreateRideDTO rideCreateDTO)
        {
            try
            {
                var response = await _api.RidePOSTAsync(rideCreateDTO);
                return new ClientResponse<RideResponseDTO>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<RideResponseDTO>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<RideResponseDTO>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<string> CancelRide(int id)
        {
            return await _api.RideDELETEAsync(id);
        }

        public async Task<ClientResponse<IEnumerable<RideResponseDTO>>> GetAllRides(RideFilterDTO filter)
        {
            try
            {
                var response = await _api.RideAllAsync(filter.StartLocation, filter.Destination, filter.Departure, (double?)filter.MaximumPrice,
                filter.AvailableSeats, filter.ShowCanceled); ;
                return new ClientResponse<IEnumerable<RideResponseDTO>>
                {
                    Response = response,
                    ErrorMessage = string.Empty,
                    StatusCode = 200
                };
            }
            catch (ApiException ex)
            {
                return new ClientResponse<IEnumerable<RideResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = ex.Response,
                    StatusCode = ex.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<IEnumerable<RideResponseDTO>>
                {
                    Response = null,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
