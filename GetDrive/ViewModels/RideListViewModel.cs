using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetDrive.Models;
using GetDrive.Models.ApiModels;

namespace GetDrive.ViewModels
{
    public partial class RideListViewModel : ViewModelBase
    {
        private readonly IRideClient rideClient;
        private readonly IMapper mapper;

        [ObservableProperty]
        private IList<RideListModel>? items;

        public RideListViewModel(IRideClient rideClient, IMapper mapper)
        {
            this.rideClient = rideClient;
            this.mapper = mapper;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            var rides = await rideClient.GetAllRides(new RideFilterDTO());
            Items = mapper.Map<IEnumerable<RideListModel>>(rides).ToList();
        }

        [RelayCommand]
        private void GoToDetail(int id)
        {
        }

        [RelayCommand]
        private void GoToFilter()
        {
        }
    }
}
