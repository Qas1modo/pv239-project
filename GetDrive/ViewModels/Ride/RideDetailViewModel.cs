using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Controls;
using GetDrive.Models;
using GetDrive.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace GetDrive.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class RideDetailViewModel : ViewModelBase
    {
        private readonly IRoutingService routingService;
        private readonly IGeocodingService geocodingService;
        private readonly IRideClient rideClient;
        private readonly IMapper mapper;

        public int Id { get; set; }

        private RideDetailModel ride;

        public RideDetailModel Ride
        {
            get => ride;
            set => SetProperty(ref ride, value);
        }

        [ObservableProperty]
        private CustomMap rideMap = new();

        [ObservableProperty]
        private bool isDriver;

        partial void OnRideMapChanged(CustomMap value)
        {
            OnPropertyChanged(nameof(RideMap));
        }

        public RideDetailViewModel(IRoutingService routingService, IRideClient rideClient, IMapper mapper, IGeocodingService geocodingService)
        {
            this.routingService = routingService;
            this.rideClient = rideClient;
            this.mapper = mapper;
            this.geocodingService = geocodingService;
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
                // TODO handle this result
                var result = await rideClient.CancelRide(Ride.Id);
                var profileRoute = routingService.GetRouteByViewModel<ProfileViewModel>();
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
            var startLocationCoordinates = await geocodingService.GetLocationAsync(startLocation);
            var destinationLocationCoordinates = await geocodingService.GetLocationAsync(destination);

            if (startLocationCoordinates != null && destinationLocationCoordinates != null)
            {
                var startPin = new Pin
                {
                    Label = "Start Location",
                    Address = startLocation,
                    Type = PinType.Place,
                    Location = startLocationCoordinates
                };
                var destinationPin = new Pin
                {
                    Label = "Destination",
                    Address = destination,
                    Type = PinType.Place,
                    Location = destinationLocationCoordinates
                };

                if (RideMap != null)
                {
                    RideMap.Pins.Clear();
                    RideMap.Pins.Add(startPin);
                    RideMap.Pins.Add(destinationPin);

                    var route = new Polyline
                    {
                        StrokeColor = Colors.Blue,
                        StrokeWidth = 12
                    };

                    route.Geopath.Clear();
                    route.Geopath.Add(new Location(startLocationCoordinates.Latitude, startLocationCoordinates.Longitude));
                    route.Geopath.Add(new Location(destinationLocationCoordinates.Latitude, destinationLocationCoordinates.Longitude));

                    RideMap.MapElements.Clear();
                    RideMap.MapElements.Add(route);
                    RideMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location((startLocationCoordinates.Latitude + destinationLocationCoordinates.Latitude) / 2, (startLocationCoordinates.Longitude + destinationLocationCoordinates.Longitude) / 2), Distance.FromKilometers(50)));
                }
            }
        }
    }
}