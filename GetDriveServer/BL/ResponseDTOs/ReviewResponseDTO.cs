using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Score { get; set; }
        public string? ReviewText { get; set; }
    }
}
