using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_Service_Ver2.Model
{
    class JobAssignmentDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");


        /*
         //READ ALL AVAILABLE CONTRACTORS BASED ON:
         - Contractor's available days
         - Contractor's timeslot
         - Contractor's preferred location
         - Contractor's skills
             */
        public static ObservableCollection<ContractorAvailable> GetAllAvailableContractors(String preferredTime, DateTime date, string suburb, int skill)
        {

            string strQuery = "select DISTINCT contractor.contractorId, contractor.firstName, contractor.surName, timeslot.StartTime, timeslot.EndTime " +
                "FROM contractor, preferredlocation, contractorskill, availability, timeslot " +
                "WHERE contractor.ContractorId = preferredlocation.ContractorId " +
                "AND contractor.ContractorId = contractorskill.ContractorId " +
                "AND contractor.ContractorId = availability.ContractorId " +
                "AND availability.SlotId = timeslot.SlotId " +
                "AND('" + preferredTime + "' >= timeslot.StartTime AND '" + preferredTime + "' <= timeslot.EndTime) " +
                "AND availability.DayId = " + (int)date.DayOfWeek +
                " AND contractorskill.SkillId = " + skill +
                " and preferredlocation.Suburb = '" + suburb  + "'";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute query

            //display all data of the available contractors found
            var temp = new ObservableCollection<ContractorAvailable>();
            foreach (DataRow dr in dt.Rows)
            {
                ContractorAvailable contractorAvailable = new ContractorAvailable()
                {
                    contractorId = Convert.ToInt32(dr[0]),
                    Firstname = dr[1].ToString(),
                    Surname = dr[2].ToString(),
                    startTime = dr[3].ToString(),
                    endTime = dr[4].ToString()
                };
                temp.Add(contractorAvailable);
            }

            //a new instance of the observable collection of contractor available
            var contractors = new ObservableCollection<ContractorAvailable>();

            if(temp.Count == 0) //if there are no available contractors found, show:
            {
                MessageBox.Show("No Available Contractors found!");
            }
            else //if there are available contractors found then:
            {
                contractors = temp;
            }

            return contractors;
        }
       
        //INSERT ASSIGNED BOOKING OF THE SELECTED CONTRATOR/S
        public static int insertAssignBooking(int bookingId, int clientId, int contractorId)
        {
            int rowsaffected;
            //insert bookingId, clientId and contractorId of the selected job and selected contractor/s
            string strBookingQuery = "insert INTO service_booking(BookingId, ClientId, ContractorId, Status) VALUES(@bookingId, @clientId, @contractorId, @status)";

            //parameters to be executed
            MySqlParameter[] param = new MySqlParameter[4];
            param[0] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[0].Value = bookingId;
            param[1] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[1].Value = clientId;
            param[2] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[2].Value = contractorId;
            param[3] = new MySqlParameter("@status", MySqlDbType.VarChar);
            param[3].Value = "Pending"; //once the booking is assigned, their status is updated from 'Awaiting for Approval' to 'Pending'

            rowsaffected = _DB.NonQuerySql(strBookingQuery, param); //execute query and params

            return rowsaffected;
        }

        public static int updateCoordinatorId(int coordinatorId, int bookingId, int clientId)
        {
            int rowsAffected;
            string query = "update booking SET CoordinatorId = @coordinatorId WHERE BookingId  = @bookingId AND ClientId = @clientId";

            MySqlParameter[] param = new MySqlParameter[3];
            param[0] = new MySqlParameter("@coordinatorId", MySqlDbType.Int32);
            param[0].Value = coordinatorId;
            param[1] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[1].Value = bookingId;
            param[2] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[2].Value = clientId;

            rowsAffected = _DB.NonQuerySql(query, param);

            return rowsAffected;

        }

    }
}
