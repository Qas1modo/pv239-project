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
        private int id;
        private string startLocation;
        private string destination;
        private int maxPassengerCount;
        private int availableSeats;
        private decimal price;
        private DateTime departure;
        private string driverNote;
        private bool canceled;

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

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
            set => SetProperty(ref maxPassengerCount, value);
        }

        public int AvailableSeats
        {
            get => availableSeats;
            set => SetProperty(ref availableSeats, value);
        }

        public decimal Price
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

        public bool Canceled
        {
            get => canceled;
            set => SetProperty(ref canceled, value);
        }
    }
}
