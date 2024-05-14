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
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class RideDetailViewModel : IViewModel
    {
        private readonly IRoutingService routingService;
        private readonly IRideClient rideClient;
        private readonly IMapper mapper;

        public int Id { get; set; }

        private RideDetailModel? ride = new();



        public RideDetailViewModel(IRoutingService routingService, IRideClient rideClient, IMapper mapper)
        {
            this.routingService = routingService;
            this.rideClient = rideClient;
            this.mapper = mapper;
        }

        public async Task OnAppearingAsync()
        {
            //await base.OnAppearingAsync();
            //var rides = await rideClient.GetAllRides(CurrentFilter);
            //Items = mapper.Map<IEnumerable<RideListModel>>(rides.Response).ToList();
        }

     

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
