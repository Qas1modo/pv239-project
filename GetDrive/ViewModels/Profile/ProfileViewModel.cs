using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Api;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Services;
using GetDrive.Views;
using System.Threading.Tasks;

namespace GetDrive.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class ProfileViewModel : ViewModelBase
    {
        private readonly IRoutingService _routingService;
        private readonly IGlobalExceptionService _globalExceptionService;
        private readonly IUserClient _userClient;
        private readonly IReviewClient _reviewClient;
        private readonly IMapper _mapper;

        public int Id { get; set; }

        [ObservableProperty]
        private ProfileModel profile = new();

        [ObservableProperty]
        private bool isNotCurrentUser;

        public ProfileViewModel(IUserClient userClient, IMapper mapper, IRoutingService routingService, IGlobalExceptionService globalExceptionService)
        {
            _userClient = userClient;
            _mapper = mapper;
            _routingService = routingService;
            _globalExceptionService = globalExceptionService;
            InitializeData();
        }

        public async Task InitializeData()
        {
            var userIdString = await SecureStorage.GetAsync("UserId");
            if (!int.TryParse(userIdString, out int userId))
            {
                var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
                await Shell.Current.GoToAsync(authRoute);
                IsNotCurrentUser = false;
            }
            else
            {
                IsNotCurrentUser = Id != userId;
            }
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
        public async Task GoToReview()
        {
            var reviewPage = _routingService.GetRouteByViewModel<ReviewViewModel>();
            await Shell.Current.GoToAsync(reviewPage);
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}