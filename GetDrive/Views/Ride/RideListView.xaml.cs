using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RideListView
    {
        public RideListView(
            RideListViewModel rideListViewModel,
            IGlobalExceptionService globalExceptionService)
            : base(rideListViewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = rideListViewModel;
        }
    }
}
