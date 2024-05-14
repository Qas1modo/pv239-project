using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models
{
    public class RideRequestsModel : ModelBase
    {
        private bool incoming;
        private bool outgoing;
        private string message;

        public bool Incoming
        {
            get => incoming;
            set => SetProperty(ref incoming, value);
        }

        public bool Outgoing
        {
            get => outgoing;
            set => SetProperty(ref outgoing, value);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
    }
}
