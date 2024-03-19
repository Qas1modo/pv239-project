using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Ride: BaseEntity
    {
        [Required]
        public int DriverId { get; set; }

        [ForeignKey("DriverId")]
        public virtual User Driver { get; set; }

        [Required]
        public string Start { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public int MaxPassangerCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        public string Note { get; set; }
    }
}
