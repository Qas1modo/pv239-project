using CommunityToolkit.Mvvm.ComponentModel;
using CookBook.Mobile.ViewModels;
using GetDrive.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.ViewModels.Ride
{
    public partial class RideViewModel : ViewModelBase
    {
        private readonly IRideClient rideClient;

        [ObservableProperty]
        private IList<RideViewModel>? items;

        public RideViewModel(IRideClient rideClient)
        {
            this.rideClient = rideClient;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            var rides = await rideClient.GetAllRides(new Models.ApiModels.RideFilterDTO());
            rides.Map
            Items = .ToList();
        }

        [RelayCommand]
        private void GoToDetail(Guid id)
        {
        }

        [RelayCommand]
        private void GoToCreate()
        {
        }
    }
}
