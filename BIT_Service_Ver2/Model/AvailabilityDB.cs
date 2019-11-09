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
    class AvailabilityDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        //Get all Contractor Availability Details
        public static ObservableCollection<Availability> GetAllAvailability()
        {

            string strQuery = "SELECT contractor.ContractorId, availability.DayId, day.DayName, availability.SlotId, timeslot.StartTime, timeslot.EndTime " +
                "FROM contractor, availability, timeslot, day " +
                "WHERE contractor.ContractorId = availability.ContractorId " +
                "AND availability.SlotId = timeslot.SlotId " +
                "AND availability.DayId = day.DayId " +
                "ORDER BY contractor.ContractorId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<Availability>();
            foreach (DataRow dr in dt.Rows)
            {
                Availability availability = new Availability()
                {
                    ContractorID = Convert.ToInt32(dr[0]),
                    DayId = Convert.ToInt32(dr[1]),
                    dayName = dr[2].ToString(),
                    SlotId = Convert.ToInt32(dr[3]),
                    starT = dr[4].ToString(),
                    endT = dr[5].ToString()
                };
                temp.Add(availability);
            }
            return temp;
        }
    }
}
