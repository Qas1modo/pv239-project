using GetDrive.Models;
using GetDrive.ViewModels;
using GetDrive.Views;

namespace GetDrive.Services
{
    public class RoutingService : IRoutingService
    {

        public IEnumerable<RouteModel> Routes => new List<RouteModel>
        {
            new("//ridelistview", typeof(RideListView), typeof(RideListViewModel)),
            new("//ridepublishview", typeof(RidePublishView), typeof(RidePublishViewModel)),
            new("//ridefilterview", typeof(RideFilterView), typeof(RideListViewModel)),
            new("//auth", typeof(Auth), typeof(AuthViewModel)),
            new("//profile", typeof(Profile), typeof(ProfileViewModel))
        };

        public string GetRouteByViewModel<TViewModel>() where TViewModel : IViewModel
            => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
    }
}
