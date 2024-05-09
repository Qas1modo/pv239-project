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
            var response = await _rideClient.CreateRide(new CreateRideDTO());
        }
    }
}
