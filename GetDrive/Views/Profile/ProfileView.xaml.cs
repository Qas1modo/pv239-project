using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class ProfileView : ContentPageBase
    {
        public ProfileView(ProfileViewModel viewModel, IGlobalExceptionService globalExceptionService)
            : base(viewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}