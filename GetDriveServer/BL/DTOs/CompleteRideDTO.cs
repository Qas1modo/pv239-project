using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CompleteRideDTO
    { 
        public int RideId { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public int MaxPassangerCount { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public string DriverNote { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public decimal AverageReviewScore { get; set; }
        
    }
}
