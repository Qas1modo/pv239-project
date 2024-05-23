using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Controls;
using GetDrive.Models;
using GetDrive.Services;
using GetDrive.Views;
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
                var profileRoute = routingService.GetRouteByViewModel<ManageProfileViewModel>();
                await Shell.Current.GoToAsync(profileRoute);
            }
        }

        [RelayCommand]
        public async Task GoToReview()
        {
            var reviewPage = new ReviewView(this);
            await Shell.Current.Navigation.PushAsync(reviewPage);
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task ShowRouteOnMap(string startLocation, string destination)
        {
            var startLocationCoords = await geocodingService.GetLocationAsync(startLocation);
            var destinationLocationCoords = await geocodingService.GetLocationAsync(destination);

            if (startLocationCoords != null && destinationLocationCoords != null)
            {
                var startPin = new Pin
                {
                    Label = "Start Location",
                    Address = startLocation,
                    Type = PinType.Place,
                    Location = startLocationCoords
                };
                var destinationPin = new Pin
                {
                    Label = "Destination",
                    Address = destination,
                    Type = PinType.Place,
                    Location = destinationLocationCoords
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
                    route.Geopath.Add(new Location(startLocationCoords.Latitude, startLocationCoords.Longitude));
                    route.Geopath.Add(new Location(destinationLocationCoords.Latitude, destinationLocationCoords.Longitude));

                    RideMap.MapElements.Clear();
                    RideMap.MapElements.Add(route);
                    Location mapLocation = new Location((startLocationCoords.Latitude + destinationLocationCoords.Latitude) / 2, (startLocationCoords.Longitude + destinationLocationCoords.Longitude) / 2);
                    RideMap.MoveToRegion(MapSpan.FromCenterAndRadius(mapLocation, Distance.FromKilometers(50)));
                }
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