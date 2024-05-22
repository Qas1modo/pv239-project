using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Api;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Models.ApiModels;
using GetDrive.Services;
using GetDrive.Views;

namespace GetDrive.ViewModels
{
    public partial class RidePublishViewModel : ViewModelBase
    {
        private readonly IRoutingService _routingService;
        private readonly IRideClient _rideClient;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private RidePublishModel ride = new();

        [ObservableProperty]
        private DateTime departureDate = DateTime.Today;

        [ObservableProperty]
        private TimeSpan departureTime = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private string messageColour = "#000000";

        public RidePublishViewModel(IRoutingService routingService, IRideClient rideClient, IMapper mapper)
        {
            _routingService = routingService;
            _rideClient = rideClient;
            _mapper = mapper;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
        }

        [RelayCommand]
        private async Task RidePublishAsync()
        {
            Ride.Departure = CombineDateTime(DepartureDate, DepartureTime);
            var createRideDTO = _mapper.Map<CreateRideDTO>(Ride);
            var result = await _rideClient.CreateRide(createRideDTO);
            MessageColour = "#FF0000";
            if (result.StatusCode == 200)
            {
                Message = "Ride successfully created";
                MessageColour = "#00FF00";
            }
            if (result.StatusCode == 401)
            {
                Message = "You must sign in before you can create the ride.";
            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                Message = result.ErrorMessage;
            }
            else
            {
                Message = "Unknown error!";
            }
        }

        private DateTime CombineDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
