using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class ReviewView : ContentPage
    {
        public ReviewView(RideDetailViewModel rideDetailViewModel)
        {
            InitializeComponent();
            BindingContext = rideDetailViewModel;
        }
    }
}
