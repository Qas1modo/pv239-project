using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models
{
    public partial class RideListModel : ModelBase
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        public string DriverName { get; set; }

        public string DriverEmail { get; set; }

        public string DriverPhone { get; set; }

        public int AvailableSeats { get; set; }

        public string StartLocation { get; set; }

        public string Destination { get; set; }

        public int MaxPassengerCount { get; set; }

        public double Price { get; set; }

        public System.DateTime Departure { get; set; }

        public string DriverNote { get; set; }

        public bool Canceled { get; set; }

        //public List<ReviewListModel> Reviews { get; set; } = new List<ReviewListModel>();
        
        //public System.Collections.Generic.ICollection<PassengerResponseDTO> Passengers { get; set; }

        //public System.Collections.Generic.ICollection<ReviewResponseDTO> DriverReviews { get; set; }
    }
}
