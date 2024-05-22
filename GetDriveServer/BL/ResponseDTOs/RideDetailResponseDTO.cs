using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class RideDetailResponseDTO
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
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public string? DriverNote { get; set; }
        public bool Canceled { get; set; }
        public IEnumerable<PassengerResponseDTO> Passengers { get; set; }
        public IEnumerable<ReviewResponseDTO> DriverReviews { get; set; }
    }
}
