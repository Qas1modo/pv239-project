using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RegistrationDTO
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email too long!")]
        public string Name { get; set; }


        [Required, EmailAddress]
        [StringLength(64, ErrorMessage = "Email too long!")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The new password must be at least 8 characters long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string RepeatPassword { get; set; }
    }
}
