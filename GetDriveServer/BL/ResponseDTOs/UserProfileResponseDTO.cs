using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class UserProfileResponseDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Score { get; set; }
        public IEnumerable<ReviewResponseDTO> Reviews { get; set; }
        public IEnumerable<PassengerDetailResponseDTO> PassengerRides { get; set; }
        public IEnumerable<RideResponseDTO> DriverRides { get; set; }
    }
}
