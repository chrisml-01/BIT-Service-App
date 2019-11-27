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
       
        public static ObservableCollection<Days> GetAllAvailability(Contractor c)
        {
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = c.contractorID;

            string strQuery = "SELECT availability.DayId, day.DayName " +
                "FROM availability, day, contractor " +
                "WHERE availability.DayId = day.DayId " +
                "AND availability.ContractorId = contractor.ContractorId " +
                "AND contractor.ContractorId = @contractorId";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery,param);

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

        public static ObservableCollection<Days> GetAvail()
        {


            string strQuery = "SELECT availability.DayId, day.DayName " +
                "FROM availability, day, contractor " +
                "WHERE availability.DayId = day.DayId " +
                "AND availability.ContractorId = contractor.ContractorId;";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

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
            
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@contractorId", MySqlDbType.Int32);
            param[0].Value = c.contractorID;

            string strQuery = "SELECT skills.SkillId , skills.SkillName " +
                "FROM contractor, contractorskill, skills " +
                "WHERE contractor.ContractorId = contractorskill.ContractorId " +
                "AND contractorskill.SkillId = skills.SkillId " +
                "AND contractor.ContractorId = @contractorId;";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery,param);

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

        public static ObservableCollection<TimeSlot> GetAllTime()
        {

            string strQuery = "SELECT SlotId, StartTime, EndTime FROM 	timeslot";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

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

            string query = "INSERT INTO contractorskill(ContractorId, SkillId) VALUES (" + contractorId + ", " + skillId + ")";

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }
      
        public static int deleteSkills(int contractorId)
        {
            int rowsAffected;

            string query = "DELETE FROM contractorskill WHERE ContractorId = " + contractorId;

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }

        //CRUD PART OF AVAILABILITY
        public static int insertDays(int contractorId, int dayId)
        {
            int rowsAffected;

            string query = "INSERT INTO availability(ContractorId, DayId, SlotId) VALUES (" + contractorId + ", " + dayId + ", NULL)";

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }

        public static int deleteDays(int contractorId)
        {
            int rowsAffected;

            string query = "DELETE FROM availability WHERE ContractorId = " + contractorId;

            rowsAffected = _DB.NonQuerySql(query);

            return rowsAffected;
        }
    }
}
