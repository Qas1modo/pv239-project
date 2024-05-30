using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Api;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using GetDrive.Views;
using System.Collections.ObjectModel;

namespace GetDrive.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class RideDetailViewModel : ViewModelBase
    {
        private readonly IRoutingService routingService;
        private readonly IRideClient rideClient;
        private readonly IUserRideClient userRideClient;
        private readonly IMapper mapper;

        public int Id { get; set; }

        [ObservableProperty]
        private RideDetailModel ride;

        [ObservableProperty]
        private PassengerDTO rideRequest = new();

        [ObservableProperty]
        private ObservableCollection<PinModel> routeCoordinates;

        [ObservableProperty]
        private bool isDriver;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private string messageColour = "#000000";

        public RideDetailViewModel(IRoutingService routingService, IRideClient rideClient,
            IMapper mapper, IUserRideClient userRideClient)
        {
            this.routingService = routingService;
            this.rideClient = rideClient;
            this.mapper = mapper;
            this.userRideClient = userRideClient;
            RouteCoordinates = new ObservableCollection<PinModel>(); // please this is required for proper Map function
        }

        public override async Task OnAppearingAsync()
        {
            var rideDetailDTO = await rideClient.GetRide(Id);
            if (rideDetailDTO.Response != null)
            {
                Ride = mapper.Map<RideDetailModel>(rideDetailDTO.Response);
                await ShowRouteOnMap(Ride.StartLocation, Ride.Destination);
                await CheckIfUserIsDriver();
                await base.OnAppearingAsync();
            }
            else
            {
                await NavigateBack();
            }
        }

        [RelayCommand]
        private async Task RideRequestCreateAsync()
        {
            RideRequest.RideId = Id;
            var result = await userRideClient.RequestRide(RideRequest);
            MessageColour = "#FF0000";
            if (result.StatusCode == 200)
            {
                Message = result.Response ?? "Ride successfully sent";
                MessageColour = "#00FF00";
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

        private async Task CheckIfUserIsDriver()
        {
            var userId = await SecureStorage.GetAsync("UserId");
            IsDriver = Ride.DriverId.ToString() == userId;
        }

        [RelayCommand]
        private async Task DeleteRideAsync()
        {
            if (Ride != null && IsDriver)
            {
                var result = await rideClient.CancelRide(Ride.Id);
                var profileRoute = routingService.GetRouteByViewModel<ManageProfileViewModel>();
                await Shell.Current.GoToAsync(profileRoute);
            }
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task ShowRouteOnMap(string startLocation, string destination)
        {
            var startLocationCoords = new Location(Ride.StartLatitude, Ride.StartLongitude);
            var destinationLocationCoords = new Location(Ride.DestinationLatitude, Ride.DestinationLongitude);

            if (startLocationCoords != null && destinationLocationCoords != null)
            {
                RouteCoordinates.Clear();
                RouteCoordinates.Add(new PinModel
                {
                    Label = "Start location",
                    Address = startLocation,
                    Location = startLocationCoords
                });
                RouteCoordinates.Add(new PinModel
                {
                    Label = "Ride destination",
                    Address = destination,
                    Location = destinationLocationCoords
                });
            }
        }

        [RelayCommand]
        public async Task GoToProfile(int id)
        {
            var profileRoute = routingService.GetRouteByViewModel<ProfileViewModel>();
            await Shell.Current.GoToAsync(profileRoute, new Dictionary<string, object>
            {
                [nameof(ProfileViewModel.Id)] = id
            });
        }
    }
}