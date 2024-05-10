using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RideFilterView
    {
        public RideFilterView(
            RideListViewModel rideListViewModel,
            IGlobalExceptionService globalExceptionService)
            : base(rideListViewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = rideListViewModel;
        }
    }
}
