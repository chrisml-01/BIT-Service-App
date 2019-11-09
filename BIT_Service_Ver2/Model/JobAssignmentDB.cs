﻿using MySql.Data.MySqlClient;
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
    class JobAssignmentDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

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

            dt = _DB.executeSQL(strQuery);

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
            return temp;
        }

        public static int insertAssignBooking(JobRequest booking)
        {
            int rowsaffected;
            string strBookingQuery = "insert INTO service_booking(BookingId, ClientId, Status) VALUES(@bookingId, @clientId, @status)";

            MySqlParameter[] param = new MySqlParameter[3];
            param[0] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[0].Value = booking.bookingId;
            param[1] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[1].Value = booking.clientID;
            param[2] = new MySqlParameter("@status", MySqlDbType.VarChar);
            param[2].Value = "Pending";

            rowsaffected = _DB.NonQuerySql(strBookingQuery, param);

            return rowsaffected;
        }

        public static int insertAssignContractor(ContractorAvailable contractor, JobRequest booking)
        {
            int rowsaffected;

            string strContractorQuery = "insert INTO booking_contractors(contractorId, bookingId, clientId) VALUES(@contractorId, @bookingId, @clientId)";

            MySqlParameter[] param = new MySqlParameter[3];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = contractor.contractorId;
            param[1] = new MySqlParameter("@bookingId", MySqlDbType.Int32);
            param[1].Value = booking.bookingId;
            param[2] = new MySqlParameter("@clientId", MySqlDbType.Int32);
            param[2].Value = booking.clientID;

            rowsaffected = _DB.NonQuerySql(strContractorQuery, param);

            return rowsaffected;


        }
    }
}