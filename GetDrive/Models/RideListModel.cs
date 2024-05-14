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
        public string StartLocation { get; set; }
        public string Destination { get; set; }
        public int MaxPassengerCount { get; set; }
        public int AvailableSeats { get; set; }
        public double Price { get; set; }
        public DateTime Departure { get; set; }
        public string DriverNote { get; set; }
        public bool Canceled { get; set; }
    }
}
