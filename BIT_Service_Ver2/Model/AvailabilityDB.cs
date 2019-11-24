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
        public static DataTable GetAllAvail()
        {

            string strQuery = "SELECT availability.DayId, day.DayName, availability.SlotId, timeslot.StartTime, timeslot.EndTime " +
                "FROM availability, timeslot, day, contractor " +
                "WHERE availability.SlotId = timeslot.SlotId " +
                "AND availability.DayId = day.DayId " +
                "AND availability.ContractorId = contractor.ContractorId " +
                "AND contractor.ContractorId = @contractorId";

            Availability availability = new Availability();
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = availability.ContractorID;

            return _DB.executeSQL(strQuery, param);
        }

        public static ObservableCollection<Availability> GetAllAvailability(int contractorId)
        {

            string strQuery = "SELECT availability.DayId, day.DayName " +
                "FROM availability, day, contractor " +
                "WHERE availability.DayId = day.DayId " +
                "AND availability.ContractorId = contractor.ContractorId " +
                "AND contractor.ContractorId = " + contractorId;

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<Availability>();
            foreach (DataRow dr in dt.Rows)
            {
                Availability availability = new Availability()
                {
                    DayId = Convert.ToInt32(dr[0]),
                    dayName = dr[1].ToString(),
                    SlotId = Convert.ToInt32(dr[2]),
                    starT = dr[3].ToString(),
                    endT = dr[4].ToString()
                };
                temp.Add(availability);
            }
            return temp;


        }

        public static ObservableCollection<ContractorSkills> GetAllContractorSkills()
        {

            string strQuery = "SELECT contractor.ContractorId, skills.SkillId , skills.SkillName " +
                "FROM contractor, contractorskill, skills " +
                "WHERE contractor.ContractorId = contractorskill.ContractorId " +
                "AND contractorskill.SkillId = skills.SkillId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<ContractorSkills>();
            foreach (DataRow dr in dt.Rows)
            {
                ContractorSkills skills = new ContractorSkills()
                {
                    ContractorID = Convert.ToInt32(dr[0]),
                    SkillId = Convert.ToInt32(dr[1]),
                    skillName = dr[2].ToString()
                };
                temp.Add(skills);
            }
            return temp;
        }

        public static ObservableCollection<PreferredLocation> GetAllPreferredLocation()
        {

            string strQuery = "SELECT preferredlocation.ContractorId, preferredlocation.Suburb " +
                "FROM contractor, preferredlocation " +
                "WHERE contractor.ContractorId = preferredlocation.ContractorId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<PreferredLocation>();
            foreach (DataRow dr in dt.Rows)
            {
                PreferredLocation location = new PreferredLocation()
                {
                    ContractorID = Convert.ToInt32(dr[0]),
                    Suburb = dr[1].ToString()
                };
                temp.Add(location);
            }
            return temp;
        }

        public static ObservableCollection<Days> GetAllDays()
        {

            string strQuery = "SELECT day.DayId, day.DayName FROM day";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

            var temp = new ObservableCollection<Days>();
            foreach (DataRow dr in dt.Rows)
            {
                Days day = new Days()
                {
                    dayId = Convert.ToInt32(dr[0]),
                    dayName = dr[1].ToString()
                };
                temp.Add(day);
            }
            return temp;
        }
    }
}
