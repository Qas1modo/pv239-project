using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RidePublishView
    {
        public RidePublishView(
            RidePublishViewModel ridePublishViewModel,
            IGlobalExceptionService globalExceptionService)
            : base(ridePublishViewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = ridePublishViewModel;
        }
    }
}
