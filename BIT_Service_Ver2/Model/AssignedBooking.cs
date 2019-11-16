using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class AssignedBooking
    {
        public int bookingId { get; set; }
        public int clientId { get; set; }
        public int contractorId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string status { get; set; }
    }
}
