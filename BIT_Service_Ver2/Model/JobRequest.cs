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

        //public JobRequest(DataRow drJobReq)
        //{
        //    MapJobRequestProperties(drJobReq);
        //}
        //private void MapJobRequestProperties(DataRow dr)
        //{
        //    this.bookingId = Convert.ToInt32(dr["bookingId"].ToString());
        //    this.clientID = Convert.ToInt32(dr["clientId"].ToString());
        //    this.skillID = Convert.ToInt32(dr["skillId"].ToString());
        //    this.serviceName = dr["serviceName"].ToString();
        //    this.bookingDate = Convert.ToDateTime(dr["bookingDate"].ToString());
        //    this.preferredTime = dr["preferredTime"].ToString();
        //    this.street = dr["street"].ToString();
        //    this.suburb = dr["suburb"].ToString();
        //    this.state = dr["state"].ToString();
        //    this.postcode = dr["postcode"].ToString();
        //    this.status = dr["status"].ToString();
        //    this.notes = dr["notes"].ToString();
        //    this.startTime = dr["startTime"].ToString();
        //    this.endTime = dr["endTime"].ToString();
        //}
    }
}
