using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class ManageProfileView : ContentPageBase
    {
        public ManageProfileView(ManageProfileViewModel viewModel, IGlobalExceptionService globalExceptionService)
            : base(viewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}