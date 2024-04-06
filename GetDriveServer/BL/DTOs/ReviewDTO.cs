using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ReviewDTO
    {
        [Required]
        public int RideId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public string ReviewText { get; set; }
        [Required]
        [Range(1, 5)]
        public int Score { get; set; }
    }
}
