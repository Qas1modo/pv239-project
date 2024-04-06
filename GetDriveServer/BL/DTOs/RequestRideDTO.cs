using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RequestRideDTO
    {
        public int RideId { get; set; }
        public int UserId { get; set; }
        public int PassangerCount { get; set; }
        public string PassangerNote { get; set; }
    }
}
