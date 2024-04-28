using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models.ApiModels
{
    public class RideFilterDTO
    {
        public string? StartLocation { get; set; }
        public string? Destination { get; set; }
        public DateTime? Departure { get; set; }
        public decimal? MaximumPrice { get; set; }
        public int? AvailableSeats { get; set; }
        public bool? ShowCanceled { get; set; }
    }
}
