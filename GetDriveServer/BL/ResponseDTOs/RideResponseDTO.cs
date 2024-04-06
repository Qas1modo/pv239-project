using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class RideResponseDTO
    {
        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string Destination { get; set; }
        public int MaxPassengerCount { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public string? DriverNote { get; set; }
        public bool Canceled { get; set; }
    }
}
