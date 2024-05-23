using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Api;
using AutoMapper;
using GetDrive.Models;
using GetDrive.Services;
using System.Diagnostics;


namespace GetDrive.ViewModels;

public partial class ManageProfileViewModel : ViewModelBase
{
    private readonly IUserClient _userClient;
    private readonly IRoutingService _routingService;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private UserProfileModel userProfile = new();

    public ManageProfileViewModel(IUserClient userClient, IMapper mapper, IRoutingService routingService)
    {
        _userClient = userClient;
        _mapper = mapper;
        _routingService = routingService;
    }

    public override async Task OnAppearingAsync()
    {
        var token = await SecureStorage.GetAsync("Token");
        if (string.IsNullOrEmpty(token))
        {
            var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
            await Shell.Current.GoToAsync(authRoute);
        }

        var userIdString = await SecureStorage.GetAsync("UserId");
        if (!int.TryParse(userIdString, out int userId))
        {
            var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
            await Shell.Current.GoToAsync(authRoute);
        }
        var userProfileDto = await _userClient.GetProfile(userId);
        UserProfile = _mapper.Map<UserProfileModel>(userProfileDto.Response);
        await base.OnAppearingAsync();
    }


    [RelayCommand]
    public async Task ManageProfile()
    {
        var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
        await Shell.Current.GoToAsync(authRoute);
    }

    [RelayCommand]
    private void GoToRideDetail(int id)
    {
        var rideDetailRoute = _routingService.GetRouteByViewModel<RideDetailViewModel>();
        Shell.Current.GoToAsync(rideDetailRoute, new Dictionary<string, object>
        {
            [nameof(RideDetailViewModel.Id)] = id
        });
    }

}

