using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models
{
    public partial class RideDetailModel : ModelBase
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        public string DriverName { get; set; }

        public string DriverEmail { get; set; }

        public string DriverPhone { get; set; }

        public int AvailableSeats { get; set; }

        public string StartLocation { get; set; }

        public string Destination { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }

        public int MaxPassengerCount { get; set; }

        public double Price { get; set; }

        public System.DateTime Departure { get; set; }

        public string DriverNote { get; set; }

        public bool Canceled { get; set; }

        public List<PassengerRideDetailModel> Passengers { get; set; } = new List<PassengerRideDetailModel>();

        public List<ReviewListModel> DriverReviews { get; set; } = new List<ReviewListModel>(); 

    }
}
