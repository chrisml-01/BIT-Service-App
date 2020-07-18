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

        
        public static ObservableCollection<Days> GetAllAvailability(Contractor c)
        {
            //parameter for the contractorId of the selected contractor
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = c.contractorID;

            //Get all Contractor Availability Details of the selected contractor
            string strQuery = "SELECT availability.DayId, day.DayName " +
                "FROM availability, day, contractor " +
                "WHERE availability.DayId = day.DayId " +
                "AND availability.ContractorId = contractor.ContractorId " +
                "AND contractor.ContractorId = @contractorId";

            DataTable dt = new DataTable();
            //execute the query along with the parameter
            dt = _DB.executeSQL(strQuery,param);

            //display the dayId and dayName of the selected contractor
            var temp = new ObservableCollection<Days>();
            foreach (DataRow dr in dt.Rows)
            {
                Days availability = new Days()
                {
                    dayId = Convert.ToInt32(dr[0]),
                    dayName = dr[1].ToString()
                };
                temp.Add(availability);
            }
            return temp;
        }

        public static ObservableCollection<Skill> GetAllContractorSkills(Contractor c)
        {
            //parameter for the contractorId of the selected contractor
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = c.contractorID;

            //Get all Contractor Skills Details of the selected contractor
            string strQuery = "SELECT skills.SkillId , skills.SkillName " +
                "FROM contractor, contractorskill, skills " +
                "WHERE contractor.ContractorId = contractorskill.ContractorId " +
                "AND contractorskill.SkillId = skills.SkillId " +
                "AND contractor.ContractorId = @contractorId;";

            DataTable dt = new DataTable();
            //execute the query along with the parameter
            dt = _DB.executeSQL(strQuery,param);

            //display the skillId and skillName of the selected contractor
            var temp = new ObservableCollection<Skill>();
            foreach (DataRow dr in dt.Rows)
            {
                Skill skills = new Skill()
                {                 
                    skillID = Convert.ToInt32(dr[0]),
                    skillName = dr[1].ToString()
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
            //Get all data of the day table from the database
            string strQuery = "SELECT day.DayId, day.DayName FROM day";

            DataTable dt = new DataTable();
            //execute query
            dt = _DB.executeSQL(strQuery);

            //display all data of the day table
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

        public static ObservableCollection<TimeSlot> GetAllTime()
        {
            //Get all timeslot data from the database
            string strQuery = "SELECT SlotId, StartTime, EndTime FROM timeslot";

            DataTable dt = new DataTable();
            //execute query
            dt = _DB.executeSQL(strQuery);

            //display all the data from the timeslot table
            var temp = new ObservableCollection<TimeSlot>();
            foreach (DataRow dr in dt.Rows)
            {
                TimeSlot time = new TimeSlot()
                {
                    slotId = Convert.ToInt32(dr[0]),
                    startTime = dr[1].ToString(),
                    endTime = dr[2].ToString()
                };
                temp.Add(time);
            }
            return temp;
        }


        //CRUD PART OF SKILLS
        public static int insertSkills(int contractorId, int skillId)
        {
            int rowsAffected;

            //insert contractor skill of the selected contractor and selected skills from the checkbox
            string query = "INSERT INTO contractorskill(ContractorId, SkillId) VALUES (" + contractorId + ", " + skillId + ")";

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }
      
        public static int deleteSkills(int contractorId)
        {
            int rowsAffected;

            //delete all contractorskill of the selected contractor
            string query = "DELETE FROM contractorskill WHERE ContractorId = " + contractorId;

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }


        //CRUD PART OF AVAILABILITY
        public static int insertDays(int contractorId, int dayId)
        {
            int rowsAffected;

            //insert the contractor availability of the selected contractor along with the dayId of the selected days from the checkboxes
            string query = "INSERT INTO availability(ContractorId, DayId, SlotId) VALUES (" + contractorId + ", " + dayId + ", NULL)";

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }

        public static int deleteDays(int contractorId)
        {
            int rowsAffected;

            //delete all availability of the selected contractor
            string query = "DELETE FROM availability WHERE ContractorId = " + contractorId;

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }
    }
}
