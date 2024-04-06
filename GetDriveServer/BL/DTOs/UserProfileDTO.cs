using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class UserProfileDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Score { get; set; }
        public IEnumerable<ReviewOutDTO> Reviews { get; set; }
        public IEnumerable<RideOutDTO> PassangerRides { get; set; }
        public IEnumerable<RideOutDTO> DriverRides { get; set; }
    }
}
