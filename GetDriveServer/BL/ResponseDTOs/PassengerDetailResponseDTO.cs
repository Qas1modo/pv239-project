using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class PassengerDetailResponseDTO
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int RideId { get; set; }
        public string StartLocation { get; set; }
        public string Destination { get; set; }
        public int MaxPassengerCount { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public string? DriverNote { get; set; }
        public bool Canceled { get; set; }
        public int PassengerCount { get; set; }
        public string? PassengerNote { get; set; }
    }
}
