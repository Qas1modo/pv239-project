using CommunityToolkit.Mvvm.Input;
using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive
{
    public partial class AppShell : Shell
    {
        private readonly IRoutingService routingService;

        public AppShell(IRoutingService routingService)
        {
            this.routingService = routingService;
            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToRecipesAsync()
        {
            var route = routingService.GetRouteByViewModel<RideListViewModel>();
            await Shell.Current.GoToAsync(route);
        }
    }
}
