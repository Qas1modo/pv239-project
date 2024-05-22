using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ResponseDTOs
{
    public class ApiErrorResponseDTO
    {
        public string ErrorMessage { get; set; }

        public ApiErrorResponseDTO(string errorMessage) 
        {
            ErrorMessage = errorMessage;
        }
    }
}
