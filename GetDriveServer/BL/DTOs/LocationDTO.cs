using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class LocationDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationDTO(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
