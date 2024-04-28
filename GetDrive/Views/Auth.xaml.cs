using GetDrive.ViewModels;
using Microsoft.Maui.Controls;

namespace GetDrive.Views;

public partial class Auth : ContentPage
{
    public Auth(AuthViewModel authViewModel)
    {
        InitializeComponent();
        this.BindingContext = authViewModel;
    }
}