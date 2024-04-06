using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class AuthDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
