using GetDrive.Clients;
using GetDrive.ViewModels;
using Microsoft.Maui.Controls;

namespace GetDrive.Views;

public partial class ChangePassword : ContentPage
{
    public ChangePassword(AuthViewModel authViewModel)
    {
        InitializeComponent();
        BindingContext = authViewModel;

    }
}