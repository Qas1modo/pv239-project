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
        [MaxLength(200)]
        public string StartLocation { get; set; }

        [Required]
        [MaxLength(200)]
        public string Destination { get; set; }

        [Required]
        public int MaxPassengerCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public int AvailableSeats { get; set; }

        [MaxLength(1000)]
        public string? DriverNote { get; set; }

        public bool Canceled { get; set; }
    }
}
