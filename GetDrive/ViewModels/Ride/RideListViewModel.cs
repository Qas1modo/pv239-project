using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Models.ApiModels;
using GetDrive.Services;

namespace GetDrive.ViewModels
{
    [INotifyPropertyChanged]
    public partial class RideListViewModel : IViewModel
    {
        private readonly IRoutingService routingService;
        private readonly IRideClient rideClient;
        private readonly IMapper mapper;

        [ObservableProperty]
        private IList<RideListModel>? items;

        [ObservableProperty]
        private RideFilterDTO currentFilter = new RideFilterDTO();

        public RideListViewModel(IRoutingService routingService, IRideClient rideClient, IMapper mapper)
        {
            this.routingService = routingService;
            this.rideClient = rideClient;
            this.mapper = mapper;
        }

        public async Task OnAppearingAsync()
        {
            var rides = await rideClient.GetAllRides(CurrentFilter);
            Items = mapper.Map<IEnumerable<RideListModel>>(rides.Response).ToList();
        }

        [RelayCommand]
        private async Task GoToFilter()
        {
            await Shell.Current.GoToAsync("//ridefilterview");
        }

        [RelayCommand]
        public async Task ApplyFilters()
        {
            await Shell.Current.GoToAsync("//ridelistview");
            await RefreshRides();
        }

        [RelayCommand]
        public async Task RefreshRides()
        {
            var rides = await rideClient.GetAllRides(CurrentFilter);
            Items = mapper.Map<IEnumerable<RideListModel>>(rides.Response).ToList();
        }
    }
}
