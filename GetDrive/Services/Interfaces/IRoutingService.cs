using GetDrive.Models;
using GetDrive.ViewModels;

namespace GetDrive.Services
{
    public interface IRoutingService
    {
        IEnumerable<RouteModel> Routes { get; }

        string GetRouteByViewModel<TViewModel>()
            where TViewModel : IViewModel;
    }
}
