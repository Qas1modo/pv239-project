using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Models.ApiModels;
using GetDrive.Services;
using System.Windows.Input;

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

        public RideListViewModel(IRoutingService routingService, IRideClient rideClient, IMapper mapper)
        {
            this.routingService = routingService;
            this.rideClient = rideClient;
            this.mapper = mapper;
        }

        public async Task OnAppearingAsync()
        {
            var rides = await rideClient.GetAllRides(new RideFilterDTO());
            Items = mapper.Map<IEnumerable<RideListModel>>(rides).ToList();
        }

        [RelayCommand]
        private void GoToFilter()
        {
        }

    }
}
