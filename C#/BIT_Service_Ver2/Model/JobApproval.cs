using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class JobApproval
    {
        public int bookingId { get; set; }
        public string serviceName { get; set; }
        public DateTime bookingDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int contractorId { get; set; }
        public string contractorName { get; set; }
        public string totalHrs { get; set; }
        public string totalKms { get; set; }
    }
}
