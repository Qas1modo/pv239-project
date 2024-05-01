using GetDrive.Clients;
using GetDrive.ViewModels;
using Microsoft.Maui.Controls;

namespace GetDrive.Views;

public partial class Auth : ContentPageBase
{
    public Auth(AuthViewModel authViewModel) : base(authViewModel)
    {
        InitializeComponent();
        this.BindingContext = authViewModel;
    }
}