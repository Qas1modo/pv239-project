using GetDrive.ViewModels;

namespace GetDrive.Views;

public partial class Profile : ContentPageBase
{
    public Profile(ProfileViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
