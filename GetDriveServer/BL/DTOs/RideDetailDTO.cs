using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RideDetailDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public int MaxPassangerCount { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public string DriverNote { get; set; }
        public bool Canceled { get; set; }
        public IEnumerable<PassangerDTO> Passangers { get ; set; }
        public IEnumerable<ReviewOutDTO> Reviews { get; set; }
    }
}
