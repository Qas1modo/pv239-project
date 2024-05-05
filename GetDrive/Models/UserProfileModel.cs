namespace GetDrive.Models
{
    public class UserProfileModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Score { get; set; }
        public List<ReviewListModel> Reviews { get; set; } = new List<ReviewListModel>();
        public List<RideListModel> DriverRides { get; set; } = new List<RideListModel>();
        public List<PassengerRideListModel> PassengerRides { get; set; } = new List<PassengerRideListModel>();
    }


}