using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using AutoMapper;
using GetDrive.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using GetDrive.Api;

namespace GetDrive.ViewModels
{
    public partial class ChangePasswordViewModel : ViewModelBase
    {
        private readonly IAuthClient _authClient;
        private readonly IMapper _mapper;
        private readonly IRoutingService _routingService;

        [ObservableProperty]
        private ChangePasswordModel changePassword = new();

        public ChangePasswordViewModel(IAuthClient authClient, IMapper mapper, IRoutingService routingService)
        {
            _authClient = authClient;
            _mapper = mapper;
            _routingService = routingService;
        }

        [RelayCommand]
        public async Task ChangePasswordAsync()
        {
            if (!ChangePassword.IsValid())
            {
                ChangePassword.ChangePasswordStatusMessage = "Check your password inputs and try again.";
                return;
            }

            var changePasswordDTO = _mapper.Map<ChangePasswordDTO>(ChangePassword);
            var result = await _authClient.ChangePassword(changePasswordDTO);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.Response))
                {
                    ChangePassword.ChangePasswordStatusMessage = result.Response;
                }
                else
                {
                    ChangePassword.ChangePasswordStatusMessage = result.ErrorMessage;
                }
                return;
            }
        }

        [RelayCommand]
        public async Task HideChangePasswordModal()
        {
            var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
            await Shell.Current.GoToAsync(authRoute);
        }
    }
}