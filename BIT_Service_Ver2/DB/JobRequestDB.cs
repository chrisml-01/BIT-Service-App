using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class JobRequestDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        public static ObservableCollection<JobRequest> GetAllJob()
        {
            string strQuery = "SELECT booking.BookingId, booking.ClientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, booking.Street, booking.Suburb, booking.State, booking.PostCode, service_booking.Status, Notes, service_booking.StartTime, service_booking.EndTime , GROUP_CONCAT(contractor.FirstName,' ',contractor.SurName) as ContractorName  " +
                "FROM booking, skills, service_booking, contractor " +
                "WHERE booking.SkillId = skills.SkillId " +
                "AND booking.BookingId = service_booking.BookingId " +
                "AND service_booking.ContractorId = contractor.ContractorId " + 
                "GROUP BY booking.BookingId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);


            var temp = new ObservableCollection<JobRequest>();
            foreach (DataRow dr in dt.Rows)
            {
                JobRequest job = new JobRequest()
                {
                    bookingId = Convert.ToInt32(dr[0]),
                    clientID = Convert.ToInt32(dr[1]),
                    skillID = Convert.ToInt32(dr[2]),
                    serviceName = dr[3].ToString(),
                    bookingDate = Convert.ToDateTime(dr[4]),
                    preferredTime = dr[5].ToString(),
                    street = dr[6].ToString(),
                    suburb = dr[7].ToString(),
                    state = dr[8].ToString(),
                    postcode = dr[9].ToString(),
                    status = dr[10].ToString(),
                    notes = dr[11].ToString(),
                    startTime = dr[12].ToString(),
                    endTime = dr[13].ToString(),
                    contractorName = dr[14].ToString()
                   
                };
                temp.Add(job);
            }
            return temp;
        }

        public static ObservableCollection<JobRequest> SearchJob(string bookingId)
        {
            string strQuery = "SELECT booking.BookingId, booking.ClientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, booking.Street, booking.Suburb, booking.State, booking.PostCode, service_booking.Status, Notes, service_booking.StartTime, service_booking.EndTime , GROUP_CONCAT(contractor.FirstName,' ',contractor.SurName) as ContractorName  " +
                 "FROM booking, skills, service_booking, contractor " +
                 "WHERE booking.SkillId = skills.SkillId " +
                 "AND booking.BookingId = service_booking.BookingId " +
                 "AND service_booking.ContractorId = contractor.ContractorId " +
                 "AND booking.BookingId LIKE '%" + bookingId +"%'" +
                 "GROUP BY booking.BookingId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);


            var temp = new ObservableCollection<JobRequest>();
            foreach (DataRow dr in dt.Rows)
            {
                JobRequest job = new JobRequest()
                {
                    bookingId = Convert.ToInt32(dr[0]),
                    clientID = Convert.ToInt32(dr[1]),
                    skillID = Convert.ToInt32(dr[2]),
                    serviceName = dr[3].ToString(),
                    bookingDate = Convert.ToDateTime(dr[4]),
                    preferredTime = dr[5].ToString(),
                    street = dr[6].ToString(),
                    suburb = dr[7].ToString(),
                    state = dr[8].ToString(),
                    postcode = dr[9].ToString(),
                    status = dr[10].ToString(),
                    notes = dr[11].ToString(),
                    startTime = dr[12].ToString(),
                    endTime = dr[13].ToString(),
                    contractorName = dr[14].ToString()

                };
                temp.Add(job);
            }
            return temp;
        }

        public static ObservableCollection<JobRequest> GetAllJobTime()
        {
            string strQuery = "SELECT service_booking.StartTime, service_booking.EndTime FROM service_booking";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);


            var temp = new ObservableCollection<JobRequest>();
            foreach (DataRow dr in dt.Rows)
            {
                JobRequest job = new JobRequest()
                {
                    startTime = dr[0].ToString(),
                    endTime = dr[1].ToString()
                };
                temp.Add(job);
            }
            return temp;
        }
        public static ObservableCollection<JobRequest> GetAllUnassigned()
        {

            string strQuery = "SELECT DISTINCT booking.BookingId, booking.clientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, Street, Suburb, State, PostCode, Status, Notes" +
                " FROM booking, skills WHERE booking.SkillId = skills.SkillId AND booking.Status = 'Awaiting for Approval' ORDER BY booking.BookingDate";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<JobRequest>();
            foreach (DataRow dr in dt.Rows)
            {
                JobRequest job = new JobRequest()
                {
                    bookingId = Convert.ToInt32(dr[0]),
                    clientID = Convert.ToInt32(dr[1]),
                    skillID = Convert.ToInt32(dr[2]),
                    serviceName = dr[3].ToString(),
                    bookingDate = Convert.ToDateTime(dr[4]),
                    preferredTime = dr[5].ToString(),
                    street = dr[6].ToString(),
                    suburb = dr[7].ToString(),
                    state = dr[8].ToString(),
                    postcode = dr[9].ToString(),
                    status = dr[10].ToString(),
                    notes = dr[11].ToString()
                };
                temp.Add(job);
            }
            return temp;
        }

        public static ObservableCollection<JobApproval> GetAllJobApproval()
        {

            string strQuery = "SELECT booking.bookingId, contractor.ContractorId , CONCAT( contractor.FirstName,' ' ,contractor.SurName) AS ContractorName ,skills.SkillName, booking.BookingDate, service_booking.StartTime, service_booking.EndTime ,pay_booking.TotalHours, pay_booking.TotalDistancedTravelled ,client.ClientId, CONCAT( client.FirstName,' ' ,client.SurName) AS ClientName " +
                "FROM pay_booking, service_booking, booking, client, skills, contractor " +
                "WHERE pay_booking.BookingId = service_booking.BookingId " +
                "AND booking.BookingId = service_booking.BookingId " +
                "AND booking.ClientId = client.ClientId " +
                "AND booking.SkillId = skills.SkillId " +
                "AND service_booking.ContractorId = contractor.ContractorId " +
                "AND pay_booking.ContractorId = service_booking.ContractorId " +
                "AND service_booking.Status = 'Completed'";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<JobApproval>();
            foreach (DataRow dr in dt.Rows)
            {
                JobApproval job = new JobApproval()
                {
                    bookingId = Convert.ToInt32(dr[0]),
                    contractorId = Convert.ToInt32(dr[1]),
                    contractorName = dr[2].ToString(),
                    serviceName = dr[3].ToString(),
                    bookingDate = (DateTime)dr[4],
                    startTime = dr[5].ToString(),
                    endTime = dr[6].ToString(),
                    totalHrs = dr[7].ToString(),
                    totalKms = dr[8].ToString() + " km",
                    clientId = Convert.ToInt32(dr[9]),
                    clientName = dr[10].ToString()
                };
                temp.Add(job);
            }
            return temp;
        }



        ////CRUD PART OF JOB REQUEST MANAGEMENT

        public static int insertBooking(JobRequest job)
        {
            int rowsaffected;

            string query = "INSERT INTO booking (ClientId, SkillId, Date, BookingDate, preferredTime, Street, Suburb, State, PostCode, Notes, Status)" +
               " VALUES (@clientId, @skillId, @date, @bookingDate, @time, @street, @suburb, @state, @postcode, @notes, @status)";

            MySqlParameter[] param = new MySqlParameter[11];
            param[0] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[0].Value = job.clientID;
            param[1] = new MySqlParameter("@skillId", MySqlDbType.Int32);
            param[1].Value = job.skillID;
            param[2] = new MySqlParameter("@date", MySqlDbType.Date);
            param[2].Value = DateTime.Now;
            param[3] = new MySqlParameter("@bookingDate", MySqlDbType.Date);
            param[3].Value = job.bookingDate;
            param[4] = new MySqlParameter("@time", MySqlDbType.VarChar);
            param[4].Value = job.preferredTime;
            param[5] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[5].Value = job.street;
            param[6] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[6].Value = job.suburb;
            param[7] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[7].Value = job.state;
            param[8] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[8].Value = job.postcode;
            param[9] = new MySqlParameter("@notes", MySqlDbType.VarChar);
            param[9].Value = job.notes;
            param[10] = new MySqlParameter("@status", MySqlDbType.VarChar);
            param[10].Value = "Awaiting for Approval";

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int updateBooking(JobRequest job)
        {
            int rowsaffected;

            string query = "UPDATE booking SET ClientId = @clientId, SkillId = @skillId, " +
                "BookingDate = @bookingDate, preferredTime = @time, Street = @street, Suburb = @suburb, State = @state, " +
                "PostCode = @postcode, Notes = @notes, Status = @status WHERE BookingId = @bookingId";

            MySqlParameter[] param = new MySqlParameter[11];
            param[0] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[0].Value = job.clientID;
            param[1] = new MySqlParameter("@skillId", MySqlDbType.Int32);
            param[1].Value = job.skillID;
            param[2] = new MySqlParameter("@bookingDate", MySqlDbType.Date);
            param[2].Value = job.bookingDate;
            param[3] = new MySqlParameter("@time", MySqlDbType.VarChar);
            param[3].Value = job.preferredTime;
            param[4] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[4].Value = job.street;
            param[5] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[5].Value = job.suburb;
            param[6] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[6].Value = job.state;
            param[7] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[7].Value = job.postcode;
            param[8] = new MySqlParameter("@notes", MySqlDbType.VarChar);
            param[8].Value = job.notes;
            param[9] = new MySqlParameter("@status", MySqlDbType.VarChar);
            param[9].Value = job.status;
            param[10] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[10].Value = job.bookingId;

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int DeleteBooking(JobRequest job)
        {
            int rowsaffected;

            string query = "DELETE FROM booking WHERE BookingId = @bookingId";
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[0].Value = job.bookingId;

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        //APPROVE AND DISAPPROVE OF COMPLETED JOB
        public static int approveBooking(JobApproval job)
        {
            int rowsaffected;

            string query = "UPDATE service_booking SET service_booking.Status = 'Sent for Payment' WHERE BookingId = @bookingId AND ContractorId = @contractorId;";

            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[0].Value = job.bookingId;
            param[1] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[1].Value = job.contractorId;
            
            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int disapproveBooking(JobApproval job)
        {
            int rowsaffected;

            string query = "UPDATE service_booking SET service_booking.Status = 'Request Denied' WHERE BookingId = @bookingId AND ContractorId = @contractorId;";

            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[0].Value = job.bookingId;
            param[1] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[1].Value = job.contractorId;

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }
    }
}
