using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RideDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The start must be at least 3 characters long!")]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The destination must be at least 3 characters long!")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "MaxPassengerCount is required")]
        [Range(1, 10, ErrorMessage = "MaxPassengerCount must be at least 1 and maximum 10")]
        public int MaxPassengerCount { get; set; }

        [Required]
        [Range(0.0, 100, ErrorMessage = "Price per km cannot be larger than 100")]
        public decimal Price { get; set; }
         
        [Required(ErrorMessage = "Departure is required")]
        public DateTime Departure { get; set; }

        [StringLength(500, ErrorMessage = "Driver note too long")]
        public string? DriverNote { get; set; }
    }
}
