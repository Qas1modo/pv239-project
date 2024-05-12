using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models
{
    public partial class RidePublishModel : ModelBase
    {
        private string startLocation = string.Empty;
        private string destination = string.Empty;
        private int maxPassengerCount = 0;
        private double price = 0;
        private DateTime departure = DateTime.Now;
        private string driverNote = string.Empty;

        public string StartLocation
        {
            get => startLocation;
            set => SetProperty(ref startLocation, value);
        }

        public string Destination
        {
            get => destination;
            set => SetProperty(ref destination, value);
        }

        public int MaxPassengerCount
        {
            get => maxPassengerCount;
            set => SetProperty(ref  maxPassengerCount, value);
        }

        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public DateTime Departure
        {
            get => departure;
            set => SetProperty(ref departure, value);
        }

        public string DriverNote
        {
            get => driverNote;
            set => SetProperty(ref driverNote, value);
        }

        public bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(StartLocation) &&
                   !string.IsNullOrWhiteSpace(Destination) &&
                   int.IsPositive(MaxPassengerCount) &&
                   double.IsPositive(Price);
        }
    }
}
