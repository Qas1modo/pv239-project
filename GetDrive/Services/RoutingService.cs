using GetDrive.Models;
using GetDrive.ViewModels;
using GetDrive.Views;

namespace GetDrive.Services
{
    public class RoutingService : IRoutingService
    {
        public RoutingService() { }

        public IEnumerable<RouteModel> Routes => new List<RouteModel>
        {
            new("//rides", typeof(RideListView), typeof(RideListViewModel)),
        };

        public string GetRouteByViewModel<TViewModel>() where TViewModel : IViewModel
            => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
    }
}
