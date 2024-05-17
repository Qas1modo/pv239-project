using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views;

public partial class ChangePasswordView
{
    public ChangePasswordView(
        ChangePasswordViewModel changePasswordViewModel,
        IGlobalExceptionService globalExceptionService)
        : base(changePasswordViewModel, globalExceptionService)
    {
        InitializeComponent();
        this.BindingContext = changePasswordViewModel;
    }
}