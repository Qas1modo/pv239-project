namespace GetDrive.Models
{
    public class ProfileModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Score { get; set; }
        public List<ReviewListModel> Reviews { get; set; } = new List<ReviewListModel>();
    }


}