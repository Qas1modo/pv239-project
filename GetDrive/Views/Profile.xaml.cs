using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class Profile : ContentPageBase
    {
        public Profile(ProfileViewModel viewModel, IGlobalExceptionService globalExceptionService)
            : base(viewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}