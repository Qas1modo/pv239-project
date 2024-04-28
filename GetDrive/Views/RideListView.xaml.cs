using GetDrive.ViewModels;

namespace GetDrive.Views;

public partial class RideListView
{
    public RideListView(RideListViewModel rideListViewModel)
        : base(rideListViewModel)
    {
        InitializeComponent();
    }
}