using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Clients.Model
{
    public class ClientResponse<T>
    {
        public T? Response { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
