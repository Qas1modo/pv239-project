using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Services;
using System.Threading.Tasks;

namespace GetDrive.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class ProfileViewModel : ViewModelBase
    {
        private readonly IRoutingService _routingService;
        private readonly IUserClient _userClient;
        private readonly IMapper _mapper;

        public int Id { get; set; }

        [ObservableProperty]
        private ProfileModel profile = new();

        public ProfileViewModel(IUserClient userClient, IMapper mapper, IRoutingService routingService)
        {
            _userClient = userClient;
            _mapper = mapper;
            _routingService = routingService;
        }

        public override async Task OnAppearingAsync()
        {
            var profileDto = await _userClient.GetProfile(Id);
            if (profileDto.Response != null)
            {
                Profile = _mapper.Map<ProfileModel>(profileDto.Response);
                await base.OnAppearingAsync();
            }
            else
            {
                await NavigateBack();
            }
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}