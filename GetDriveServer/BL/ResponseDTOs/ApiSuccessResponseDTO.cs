using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class ApiSuccessResponseDTO
    {
        public string Message { get; set; }

        public ApiSuccessResponseDTO(string message)
        {
            Message = message;
        }
    }
}
