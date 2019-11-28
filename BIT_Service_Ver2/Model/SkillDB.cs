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
    class SkillDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");
        public static ObservableCollection<Skill> GetAllSkills()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            string strQuery = "SELECT * from skills";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn);

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);

            var temp = new ObservableCollection<Skill>();
            foreach (DataRow dr in dt.Rows)
            {
                Skill skill = new Skill()
                {
                    skillID = Convert.ToInt32(dr[0]),
                    skillName = dr[1].ToString(),
                    description = dr[2].ToString(),
                    charge = Convert.ToDouble(dr[3])
                    
                };
                temp.Add(skill);
            }
            return temp;
        }

        public static ObservableCollection<Skill> SearchSkills(string skillName)
        {
          
            string strQuery = "SELECT * FROM skills WHERE skills.SkillName LIKE '%" + skillName + "%'";
            

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);
           

            var temp = new ObservableCollection<Skill>();
            foreach (DataRow dr in dt.Rows)
            {
                Skill skill = new Skill()
                {
                    skillID = Convert.ToInt32(dr[0]),
                    skillName = dr[1].ToString(),
                    description = dr[2].ToString(),
                    charge = Convert.ToDouble(dr[3])

                };
                temp.Add(skill);
            }
            return temp;
        }

        public static int insertSkill(Skill skill)
        {
            int rowsaffected;

            string query = "INSERT INTO skills (SkillName, Description, ChargePerHr)" +
               " VALUES (@skillName, @description, @charge)";

            Skill addSkill = new Skill();
            MySqlParameter[] param = new MySqlParameter[3];
            param[0] = new MySqlParameter("@skillName", MySqlDbType.VarChar);
            param[0].Value = skill.skillName;
            param[1] = new MySqlParameter("@description", MySqlDbType.VarChar);
            param[1].Value = skill.description;
            param[2] = new MySqlParameter("@charge", MySqlDbType.Double);
            param[2].Value = skill.charge;
         

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int updateSkill(Skill skill)
        {
            int rowsaffected;

            string query = "UPDATE skills SET SkillName = @skillName, Description = @description, Charge = @charge WHERE SkillId = @skillId";

            Skill addSkill = new Skill();
            MySqlParameter[] param = new MySqlParameter[4];
            param[0] = new MySqlParameter("@skillName", MySqlDbType.VarChar);
            param[0].Value = skill.skillName;
            param[1] = new MySqlParameter("@description", MySqlDbType.VarChar);
            param[1].Value = skill.description;
            param[2] = new MySqlParameter("@charge", MySqlDbType.Double);
            param[2].Value = skill.charge;
            param[3] = new MySqlParameter("@skillId", MySqlDbType.Int32);
            param[3].Value = skill.skillID;


            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }
    }
}
