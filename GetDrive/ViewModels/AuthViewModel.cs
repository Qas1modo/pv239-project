using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Api;

namespace GetDrive.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    private readonly IAuthClient _authClient;

    [ObservableProperty]
    private RegistrationDTO registrationData = new();

    [ObservableProperty]
    private LoginDto loginData = new();

    [ObservableProperty]
    private bool isRegistering = true;

    [ObservableProperty]
    private bool isLoggedIn;

    [ObservableProperty]
    private string statusMessage;

    [ObservableProperty]
    private string toggleButtonText = "Switch to login form";

    public bool IsNotLoggedIn => !isLoggedIn;

    public bool IsNotRegistering => !isRegistering;


    public AuthViewModel(IAuthClient authClient)
    {
        _authClient = authClient;
        UpdateStatusMessage();
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        CheckLoginStatus();
    }

    private async void CheckLoginStatus()
    {
        var token = await SecureStorage.GetAsync("Token");
        IsLoggedIn = !string.IsNullOrEmpty(token);
        OnPropertyChanged(nameof(IsNotLoggedIn));
    }


    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (!IsRegisterFormValid())
        {
            UpdateStatusMessage("Please check your registration inputs and try again.");
            return;
        }
        try
        {
            var response = await _authClient.Register(RegistrationData);
            if (response != null && response.Token != null)
            {
                UpdateStatusMessage("Registration has been successfully done... ");
                await Task.Delay(1500);
                await Shell.Current.GoToAsync("//ridelistview");
            }
            else
            {
                UpdateStatusMessage("Registration was unsuccessful. Please check your fields.");
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
            UpdateStatusMessage("Please check your login inputs and try again.");
            return;
        }
        try
        {
            var response = await _authClient.Login(LoginData);
            if (response != null && response.Token != null && response.UserId != null)
            {
                await SecureStorage.SetAsync("Token", response.Token);
                await SecureStorage.SetAsync("UserId", response.UserId.ToString());
                IsLoggedIn = true;
                OnPropertyChanged(nameof(IsNotLoggedIn));
                UpdateStatusMessage("Login successful.");
                await Task.Delay(1500);
                await Shell.Current.GoToAsync("//ridelistview");
            }
            else
            {
                UpdateStatusMessage("Login failed. Please check your email and password.");
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Login failed: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authClient.Logout();
        IsLoggedIn = false;
        OnPropertyChanged(nameof(IsNotLoggedIn));
        UpdateStatusMessage("Successful logout.");
        await Task.Delay(1500);
        await Shell.Current.GoToAsync("//ridelistview");
    }

    [RelayCommand]
    private void ToggleMode()
    {
        IsRegistering = !IsRegistering;
        OnPropertyChanged(nameof(IsNotRegistering));
        UpdateStatusMessage();
        ToggleButtonText = IsNotRegistering ? "Switch to register form" : "Switch to login form";
    }

    private void UpdateStatusMessage(string? message = null)
    {
        StatusMessage = message ?? (IsRegistering
                                    ? "Please fill in registration entries (all entries are required)."
                                    : "Please fill in login entries.");
    }

    private bool IsRegisterFormValid()
    {
        return !string.IsNullOrWhiteSpace(RegistrationData.Name) &&
               !string.IsNullOrWhiteSpace(RegistrationData.Email) &&
               !string.IsNullOrWhiteSpace(RegistrationData.Phone) &&
               !string.IsNullOrWhiteSpace(RegistrationData.Password) &&
               RegistrationData.Password == RegistrationData.RepeatPassword &&
               RegistrationData.Password.Length >= 8;
    }

    private bool IsLoginFormValid()
    {
        return !string.IsNullOrWhiteSpace(LoginData.Email) &&
               !string.IsNullOrWhiteSpace(LoginData.Password);
    }
}