using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views;

public partial class RideRequests : ContentPageBase
{
	public RideRequests(RideRequestsViewModel incomingRideRequestsViewModel, IGlobalExceptionService globalExceptionService) 
        : base(incomingRideRequestsViewModel, globalExceptionService)
    {
        InitializeComponent();
        this.BindingContext = incomingRideRequestsViewModel;
    }
}