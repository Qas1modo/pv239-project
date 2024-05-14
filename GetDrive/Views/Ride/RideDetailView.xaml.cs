using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RideDetailView
    {
        public RideDetailView(
            RideDetailViewModel rideDetailViewModel,
            IGlobalExceptionService globalExceptionService)
            : base(rideDetailViewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = rideDetailViewModel;
        }
    }
}
