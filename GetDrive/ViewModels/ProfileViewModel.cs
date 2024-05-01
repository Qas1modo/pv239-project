using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Api;


namespace GetDrive.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private readonly IUserClient _userClient;

    [ObservableProperty]
    private UserProfileResponseDTO userProfile;

    public ProfileViewModel(IUserClient userClient)
    {
        _userClient = userClient;
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
        await LoadUserProfileAsync();
    }

    [RelayCommand]
    public async Task LoadUserProfileAsync()
    {
        try
        {
            var userIdString = await SecureStorage.GetAsync("UserId");
            if (!int.TryParse(userIdString, out int userId))
            {
                await Shell.Current.GoToAsync("//auth");
                return;
            }
            UserProfile = await _userClient.GetProfile(userId);
        }
        catch (Exception)
        {
            await Shell.Current.GoToAsync("//auth");
        }
    }
}
