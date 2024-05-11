using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RideFilterView : ContentPage
    {
        public RideFilterView(RideListViewModel rideListViewModel)
        {
            InitializeComponent();
            BindingContext = rideListViewModel;

        }
    }
}
