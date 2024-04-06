using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class PassengerDTO
    {
        [Required]
        public int RideId { get; set; }

        [Required]
        [Range(1,10)]
        public int PassengerCount { get; set; }

        [StringLength(500, ErrorMessage = "Note too long")]
        public string? PassengerNote { get; set; }
    }
}
