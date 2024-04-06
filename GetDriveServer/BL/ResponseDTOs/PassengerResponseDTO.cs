using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class PassengerResponseDTO
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int RideId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PassengerCount { get; set; }
        public string? PassengerNote { get; set; }
    }
}
