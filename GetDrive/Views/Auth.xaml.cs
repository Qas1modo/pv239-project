using GetDrive.Clients;
using GetDrive.Services;
using GetDrive.ViewModels;
using Microsoft.Maui.Controls;

namespace GetDrive.Views;

public partial class Auth : ContentPageBase
{
    public Auth(AuthViewModel authViewModel, IGlobalExceptionService globalExceptionService) : base(authViewModel, globalExceptionService)
    {
        InitializeComponent();
        this.BindingContext = authViewModel;
    }
}