using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using AutoMapper;
using GetDrive.Api;
using GetDrive.Services;
using System.Diagnostics;

namespace GetDrive.ViewModels;

public partial class AuthViewModel : ViewModelBase
{
    private readonly IAuthClient _authClient;
    private readonly IMapper _mapper;
    private readonly IRoutingService _routingService;

    [ObservableProperty]
    private AuthModel auth = new();

    public AuthViewModel(IAuthClient authClient, IMapper mapper, IRoutingService routingService)
    {
        _authClient = authClient;
        _mapper = mapper;
        _routingService = routingService;
        UpdateToggleButtonText();
    }

    public override async Task OnAppearingAsync()
    {
        await CheckLoginStatus();
        await base.OnAppearingAsync();
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
            var rideListRoute = _routingService.GetRouteByViewModel<RideListViewModel>();
            await Task.Delay(500);
            await Shell.Current.GoToAsync(rideListRoute);
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
            var rideListRoute = _routingService.GetRouteByViewModel<RideListViewModel>();
            await Task.Delay(500);
            await Shell.Current.GoToAsync(rideListRoute);
        }
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        try
        {
            await _authClient.Logout();
            Auth.IsLoggedIn = false;
            var rideListRoute = _routingService.GetRouteByViewModel<RideListViewModel>();
            await Shell.Current.GoToAsync(rideListRoute);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }



    [RelayCommand]
    public async Task GoToChangePassword()
    {
        try
        {
            var changePasswordRoute = _routingService.GetRouteByViewModel<ChangePasswordViewModel>();
            await Shell.Current.GoToAsync(changePasswordRoute);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
       
    }
}
       