using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Models.ApiModels;
using GetDrive.Services;
using GetDrive.Views;

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
        public async Task GoToFilter()
        {
            var filterPage = new RideFilterView(this);
            await Shell.Current.Navigation.PushAsync(filterPage);
        }

        [RelayCommand]
        public async Task ApplyFilters()
        {
            var rides = await rideClient.GetAllRides(CurrentFilter);
            Items = mapper.Map<IEnumerable<RideListModel>>(rides).ToList();
            await NavigateBack();
        }

        [RelayCommand]
        public async Task CancelFilters()
        {
            CurrentFilter = new RideFilterDTO();
            var rides = await rideClient.GetAllRides(CurrentFilter);
            Items = mapper.Map<IEnumerable<RideListModel>>(rides.Response).ToList();
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void GoToDetail(int id)
        {
            Shell.Current.GoToAsync("//ridedetailview", new Dictionary<string, object>
            {
                [nameof(RideDetailViewModel.Id)] = id
            });
        }

    }
}
