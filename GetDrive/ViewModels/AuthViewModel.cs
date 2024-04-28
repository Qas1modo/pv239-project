using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Api;

namespace GetDrive.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    private readonly IAuthClient _authClient;

    [ObservableProperty]
    private RegistrationDTO registrationData = new RegistrationDTO();

    [ObservableProperty]
    private LoginDto loginData = new LoginDto();

    [ObservableProperty]
    private bool isRegistering = true;

    public bool IsNotRegistering { get { return !IsRegistering; } }


    [ObservableProperty]
    private string statusMessage = "Please fill in entries (all entries are required).";
    [ObservableProperty]
    private string toggleButtonText = "Switch to Login";


    public AuthViewModel(IAuthClient authClient)
    {
        _authClient = authClient;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (!IsRegisterFormValid())
        {
            StatusMessage = "Please check your registration inputs and try again.";
            return;
        }
        try
        {
            var response = await _authClient.Register(RegistrationData);
            if (response != null && response.Token != null)
            {
                StatusMessage = "Your registration has been successfully completed.";
                await Shell.Current.GoToAsync("//ridelistview");
            }
            else
            {
                StatusMessage = "Registration was unsuccessful. Please check your fields.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Registration failed: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (!IsLoginFormValid())
        {
            StatusMessage = "Please check your login inputs and try again.";
            return;
        }
        try
        {
            var response = await _authClient.Login(loginData);
            if (response != null && response.Token != null)
            {
                StatusMessage = "Login successful.";
                await Shell.Current.GoToAsync("//ridelistview");
            }
            else
            {
                StatusMessage = "Login failed. Please check your email and password.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Login failed: {ex.Message}";
        }
    }

    [RelayCommand]
    private void ToggleMode()
    {
        IsRegistering = !IsRegistering;
        StatusMessage = isRegistering ? "Please fill in entries (all entries are required)." : "Please enter your login details.";
        ToggleButtonText = IsRegistering ? "Switch to Login" : "Switch to Register";
    }

    private bool IsRegisterFormValid()
    {
        return !string.IsNullOrWhiteSpace(registrationData.Name) &&
               !string.IsNullOrWhiteSpace(registrationData.Email) &&
               !string.IsNullOrWhiteSpace(registrationData.Phone) &&
               !string.IsNullOrWhiteSpace(registrationData.Password) &&
               registrationData.Password == registrationData.RepeatPassword &&
               registrationData.Password.Length >= 8;
    }

    private bool IsLoginFormValid()
    {
        // loginData.Password.Length >= 8; vypnute kvoli seedu
        return !string.IsNullOrWhiteSpace(loginData.Email) &&
               !string.IsNullOrWhiteSpace(loginData.Password);
    }
}