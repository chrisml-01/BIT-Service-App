using BIT_Service_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class JobRequest
    {
        private InputValidation val = new InputValidation();

        public int bookingId { get; set; }
        public int clientID { get; set; }
        public int skillID { get; set; }
        
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
        public string contractorName { get; set; }

        public int ValidateJobRequest()
        {
            int result = 1;

            if (val.BookingDate(bookingDate) == false || val.Address(street, suburb, state, postcode) == false)
            {
                result = 0;
            }
           
            return result;
        }
    }   
}
