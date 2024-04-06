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
        public int UserId { get; set; }

        [StringLength(500, ErrorMessage = "Review text too long")]
        public string? ReviewText { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Score must be between 1 and 10")]
        public int Score { get; set; }
    }
}
