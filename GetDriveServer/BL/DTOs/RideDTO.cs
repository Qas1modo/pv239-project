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
        [Required(ErrorMessage = "DriverId is requierd")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Start is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The start must be at least 3 characters long!")]
        public string Start { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The destination must be at least 3 characters long!")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "MaxPassangerCount is required")]
        [Range(1, 10, ErrorMessage = "MaxPassangerCount must be at least 1 and maximum 10")]
        public int MaxPassangerCount { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.0, 100, ErrorMessage = "Price per km cannot be larger than 100")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Departure is required")]
        public DateTime Departure { get; set; }

        public string DriverNote { get; set; }
    }
}
