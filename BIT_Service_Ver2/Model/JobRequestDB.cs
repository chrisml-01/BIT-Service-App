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
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            //string strQuery = "SELECT booking.BookingId, booking.clientId, booking.SkillId ,skills.SkillName, BookingDate, preferredTime, booking.Street,  booking.Suburb,  booking.State,  booking.PostCode, service_booking.Status, Notes, service_booking.StartTime, service_booking.EndTime, contractor.ContractorId, contractor.FirstName, contractor.SurName " +
            //    "FROM booking, skills, service_booking, booking_contractors, contractor " +
            //    "WHERE booking.SkillId = skills.SkillId " +
            //    "AND booking.BookingId = service_booking.BookingId " +
            //    "AND service_booking.BookingId = booking_contractors.BookingId " +
            //    "AND booking_contractors.ContractorId = contractor.ContractorId";

            string strQuery = "SELECT booking.BookingId, booking.clientId, booking.SkillId ,skills.SkillName, BookingDate, preferredTime, Street, Suburb, State, PostCode, service_booking.Status, Notes, service_booking.StartTime, service_booking.EndTime  " +
                "FROM booking, skills, service_booking " +
                "WHERE booking.SkillId = skills.SkillId " +
                "AND booking.BookingId = service_booking.BookingId";

            MySqlCommand cmd = new MySqlCommand(strQuery, myConn);

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);

            var temp = new ObservableCollection<JobRequest>();
            foreach (DataRow dr in dt.Rows)
            {
                JobRequest job = new JobRequest()
                {
                    bookingId = Convert.ToInt32(dr[0]),
                    clientID = Convert.ToInt32(dr[1]),
                    skillId = Convert.ToInt32(dr[2]),
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
                    endTime = dr[13].ToString()
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
                    skillId = Convert.ToInt32(dr[2]),
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

        //public static ObservableCollection<JobRequest> GetAllPending()
        //{

        //    string strQuery = "SELECT DISTINCT booking.clientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, Street, Suburb, State, PostCode, Status, Notes" +
        //        " FROM booking, skills WHERE booking.SkillId = skills.SkillId AND booking.Status = 'Pending' ORDER BY booking.BookingDate";

        //    DataTable dt = new DataTable();

        //    dt = _DB.executeSQL(strQuery);

        //    var temp = new ObservableCollection<JobRequest>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        JobRequest job = new JobRequest()
        //        {
        //            clientID = Convert.ToInt32(dr[0]),
        //            skillId = Convert.ToInt32(dr[1]),
        //            serviceName = dr[2].ToString(),
        //            bookingDate = Convert.ToDateTime(dr[3]),
        //            startTime = dr[4].ToString(),
        //            street = dr[5].ToString(),
        //            suburb = dr[6].ToString(),
        //            state = dr[7].ToString(),
        //            postcode = dr[8].ToString(),
        //            status = dr[9].ToString(),
        //            notes = dr[10].ToString()
        //        };
        //        temp.Add(job);
        //    }
        //    return temp;
        //}

        //public static ObservableCollection<JobRequest> GetAllInProgress()
        //{

        //    string strQuery = "SELECT DISTINCT booking.clientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, Street, Suburb, State, PostCode, Status, Notes" +
        //        " FROM booking, skills WHERE booking.SkillId = skills.SkillId AND booking.Status = 'In Progress' ORDER BY booking.BookingDate";

        //    DataTable dt = new DataTable();

        //    dt = _DB.executeSQL(strQuery);

        //    var temp = new ObservableCollection<JobRequest>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        JobRequest job = new JobRequest()
        //        {
        //            clientID = Convert.ToInt32(dr[0]),
        //            skillId = Convert.ToInt32(dr[1]),
        //            serviceName = dr[2].ToString(),
        //            bookingDate = Convert.ToDateTime(dr[3]),
        //            startTime = dr[4].ToString(),
        //            street = dr[5].ToString(),
        //            suburb = dr[6].ToString(),
        //            state = dr[7].ToString(),
        //            postcode = dr[8].ToString(),
        //            status = dr[9].ToString(),
        //            notes = dr[10].ToString()
        //        };
        //        temp.Add(job);
        //    }
        //    return temp;
        //}

        //public static ObservableCollection<JobRequest> GetAllComplete()
        //{

        //    string strQuery = "SELECT DISTINCT booking.clientId, booking.SkillId, skills.SkillName, BookingDate, preferredTime, Street, Suburb, State, PostCode, service_booking.Status, Notes " +
        //        "FROM booking, skills, service_booking " +
        //        "WHERE booking.SkillId = skills.SkillId " +
        //        "AND booking.BookingId = service_booking.BookingId " +
        //        "AND service_booking.Status = 'Complete'ORDER BY booking.BookingDate";

        //    DataTable dt = new DataTable();

        //    dt = _DB.executeSQL(strQuery);

        //    var temp = new ObservableCollection<JobRequest>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        JobRequest job = new JobRequest()
        //        {
        //            clientID = Convert.ToInt32(dr[0]),
        //            skillId = Convert.ToInt32(dr[1]),
        //            serviceName = dr[2].ToString(),
        //            bookingDate = Convert.ToDateTime(dr[3]),
        //            startTime = dr[4].ToString(),
        //            street = dr[5].ToString(),
        //            suburb = dr[6].ToString(),
        //            state = dr[7].ToString(),
        //            postcode = dr[8].ToString(),
        //            status = dr[9].ToString(),
        //            notes = dr[10].ToString()
        //        };
        //        temp.Add(job);
        //    }
        //    return temp;
        //}

        ////CRUD PART OF JOB REQUEST MANAGEMENT

        //public static int insertBooking(Client client)
        //{
        //    int rowsaffected;

        //    string query = "INSERT INTO client (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email)" +
        //       " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email)";

        //    Client Addclient = new Client();
        //    MySqlParameter[] param = new MySqlParameter[9];
        //    param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
        //    param[0].Value = client.FirstName;
        //    param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
        //    param[1].Value = client.SurName;
        //    param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
        //    param[2].Value = client.DOB;
        //    param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
        //    param[3].Value = client.Street;
        //    param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
        //    param[4].Value = client.Suburb;
        //    param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
        //    param[5].Value = client.State;
        //    param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
        //    param[6].Value = client.Postcode;
        //    param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
        //    param[7].Value = client.MobileNum;
        //    param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
        //    param[8].Value = client.Email;

        //    rowsaffected = _DB.NonQuerySql(query, param);

        //    return rowsaffected;
        //}
    }
}
