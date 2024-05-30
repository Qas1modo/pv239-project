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
            new("//authview", typeof(AuthView), typeof(AuthViewModel)),
            new("//manageprofileview", typeof(ManageProfileView), typeof(ManageProfileViewModel)),
            new("//ridedetailview", typeof(RideDetailView), typeof(RideDetailViewModel)),
            new("//changepassword", typeof(ChangePasswordView), typeof(ChangePasswordViewModel)),
            new("//profile", typeof(ProfileView), typeof(ProfileViewModel)),
            new("//addreview", typeof(ReviewView), typeof(ReviewViewModel))
        };

        public string GetRouteByViewModel<TViewModel>() where TViewModel : IViewModel
            => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
    }
}
