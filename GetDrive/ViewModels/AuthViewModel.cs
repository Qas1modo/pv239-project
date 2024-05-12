using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using AutoMapper;
using GetDrive.Api;
using GetDrive.Views;

namespace GetDrive.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    private readonly IAuthClient _authClient;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private AuthModel auth = new();

    public AuthViewModel(IAuthClient authClient, IMapper mapper)
    {
        _authClient = authClient;
        _mapper = mapper;
        UpdateToggleButtonText();
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        await CheckLoginStatus();
    }

    public async Task CheckLoginStatus()
    {
        var token = await SecureStorage.Default.GetAsync("Token");
        Auth.IsLoggedIn = !string.IsNullOrWhiteSpace(token);
        UpdateToggleButtonText();
    }

    [RelayCommand]
    private void ToggleMode()
    {
        Auth.IsRegistering = !Auth.IsRegistering;
        UpdateToggleButtonText();
    }

    private void UpdateToggleButtonText()
    {
        Auth.ToggleButtonText = Auth.IsRegistering ? "Switch to Login form" : "Switch to Register form";
        Auth.StatusMessage = Auth.IsRegistering ? "Please fill in registration entries." : "Please fill in login entries.";
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (!Auth.Registration.IsFormValid())
        {
            Auth.StatusMessage = "Please check your registration inputs and try again.";
            return;
        }
        var registrationDTO = _mapper.Map<RegistrationDTO>(Auth.Registration);
        var response = await _authClient.Register(registrationDTO);

        if (!string.IsNullOrEmpty(response.ErrorMessage))
        {
            Auth.StatusMessage = response.ErrorMessage;
        }
        else if (response.Response != null && response.Response.Token != null)
        {
            Auth.StatusMessage = "Registration successful.";
            await Shell.Current.GoToAsync("//ridelistview");
        }
        else
        {
            Auth.StatusMessage = "Unexpected issue occured during registration.";
        }
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        var loginDto = _mapper.Map<LoginDto>(Auth.Login);
        var response = await _authClient.Login(loginDto);

        if (!string.IsNullOrEmpty(response.ErrorMessage))
        {
            Auth.StatusMessage = response.ErrorMessage;
        } 
        else if (response.Response != null && response.Response.Token != null)
        {
            Auth.IsLoggedIn = true;
            Auth.StatusMessage = "Login successful.";
            await Shell.Current.GoToAsync("//ridelistview");
        }
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authClient.Logout();
        Auth.IsLoggedIn = false;
        await Shell.Current.GoToAsync("//ridelistview");
    }


    [RelayCommand]
    public async Task ShowChangePasswordPopup()
    {
        var changePasswordModalPage = new ChangePassword(this);
        await Application.Current.MainPage.Navigation.PushModalAsync(changePasswordModalPage);
    }

    [RelayCommand]
    public async Task ChangePasswordAsync()
    {
        if (!Auth.ChangePassword.IsValid())
        {
            Auth.ChangePasswordStatusMessage = "Check your password inputs and try again.";
            return;
        }

        var changePasswordDTO = _mapper.Map<ChangePasswordDTO>(Auth.ChangePassword);
        var result = await _authClient.ChangePassword(changePasswordDTO);
        if (result != null)
        {
            if (!string.IsNullOrEmpty(result.Response))
            {
                Auth.ChangePasswordStatusMessage = result.Response;
            }
            Auth.ChangePasswordStatusMessage = result.ErrorMessage;
            return;
        }
    }

    [RelayCommand]
    public async Task HideChangePasswordModal() => await Application.Current.MainPage.Navigation.PopModalAsync();
}