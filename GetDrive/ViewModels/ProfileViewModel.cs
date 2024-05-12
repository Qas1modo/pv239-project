using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Api;
using System.Collections.ObjectModel;
using AutoMapper;
using GetDrive.Models;


namespace GetDrive.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private readonly IUserClient _userClient;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private UserProfileModel userProfile;

    public ProfileViewModel(IUserClient userClient, IMapper mapper)
    {
        _userClient = userClient;
        _mapper = mapper;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        var token = await SecureStorage.GetAsync("Token");
        if (string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync("//auth");
            return;
        }

        var userIdString = await SecureStorage.GetAsync("UserId");
        if (!int.TryParse(userIdString, out int userId))
        {
            await Shell.Current.GoToAsync("//auth");
            return;
        }

        var userProfileDto = await _userClient.GetProfile(userId);
        UserProfile = _mapper.Map<UserProfileModel>(userProfileDto.Response);
    }

    [RelayCommand]
    public static async Task ManageProfile()
    {
        await Shell.Current.GoToAsync("//auth");
    }

}

