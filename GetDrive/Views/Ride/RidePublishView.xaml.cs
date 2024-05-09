using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class RidePublishView
    {
        public RidePublishView(
            RidePublishViewModel viewModel,
            IGlobalExceptionService globalExceptionService)
            : base(viewModel, globalExceptionService)
        {
            InitializeComponent();
        }
    }
}
