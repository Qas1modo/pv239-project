using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RideFilterDTO
    {
        [StringLength(100, ErrorMessage = "The start must be at most 100 characters long!")]
        public string? StartLocation { get; set; }

        [StringLength(100, ErrorMessage = "The destination must be at most 100 characters long!")]
        public string? Destination { get; set; }
        public DateTime? Date { get; set; }
        public decimal? MaximumPrice { get; set; }
        public int? AvailableSeats { get; set; }
        public bool? ShowCanceled { get; set; }
    }
}
