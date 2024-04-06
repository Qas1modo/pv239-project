using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ReviewOutDTO
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
