using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class JobRequest
    {
        public int bookingId { get; set; }
        public int clientID { get; set; }
        public int skillId { get; set; }
        public string serviceName { get; set; }
        public DateTime bookingDate { get; set; }
        public string preferredTime { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
