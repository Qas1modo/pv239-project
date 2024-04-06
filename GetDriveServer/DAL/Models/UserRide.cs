using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DAL.Models
{
    public class UserRide : BaseEntity
    {
        [Required]
        public int PassengerId { get; set; }

        [ForeignKey("PassengerId")]
        public virtual User Passenger { get; set; }

        [Required]
        public int RideId { get; set; }

        [ForeignKey("RideId")]
        public virtual Ride Ride { get; set; }

        [Required]
        public int PassengerCount { get; set; }
        public bool Accepted { get; set; }

        [MaxLength(100)]
        public string? PassengerNote { get; set; }
    }
}
