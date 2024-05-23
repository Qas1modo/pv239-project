using GetDrive.Clients;
using GetDrive.Services;
using GetDrive.ViewModels;
using Microsoft.Maui.Controls;

namespace GetDrive.Views;

public partial class AuthView : ContentPageBase
{
    public AuthView(AuthViewModel authViewModel, IGlobalExceptionService globalExceptionService) : base(authViewModel, globalExceptionService)
    {
        InitializeComponent();
        this.BindingContext = authViewModel;
    }
}