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
    class CoordinatorDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        //Get all Coordinator Details
        public static ObservableCollection<Coordinator> GetAllCoordinators()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            string strQuery = "select coordinatorId, FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password from Coordinator";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn);

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);

            var temp = new ObservableCollection<Coordinator>();
            foreach (DataRow dr in dt.Rows)
            {
                Coordinator coordinator = new Coordinator()
                {
                    coordinatorId = Convert.ToInt32(dr[0]),
                    FirstName = dr[1].ToString(),
                    SurName = dr[2].ToString(),
                    DOB = (DateTime)dr[3],
                    Street = dr[4].ToString(),
                    Suburb = dr[5].ToString(),
                    State = dr[6].ToString(),
                    Postcode = dr[7].ToString(),
                    MobileNum = dr[8].ToString(),
                    Email = dr[9].ToString(),
                    Username = dr[10].ToString(),
                    Password = dr[11].ToString()
                };
                temp.Add(coordinator);
            }
            return temp;
        }

        public static int insertCoordinator(Coordinator coordinator)
        {
            int rowsaffected;

            string query = "INSERT INTO coordinator (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email)" +
               " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email)";

            Coordinator Addcoordinator = new Coordinator();
            MySqlParameter[] param = new MySqlParameter[9];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = coordinator.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = coordinator.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = coordinator.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = coordinator.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = coordinator.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = coordinator.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = coordinator.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = coordinator.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = coordinator.Email;

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int updateCoordinator(Coordinator coordinator)
        {
            int rowsAffected;

            string query = "UPDATE COORDINATOR SET FirstName = @firstName, SurName = @surName, DOB = @dob, Street = @street, " +
                "Suburb = @suburb, State = @state, PostCode = @postCode, Email = @email, MobileNumber = @mobileNumber WHERE CoordinatorId = @coordinatorId";


            Coordinator Addcoordinator = new Coordinator();
            MySqlParameter[] param = new MySqlParameter[10];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = coordinator.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = coordinator.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = coordinator.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = coordinator.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = coordinator.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = coordinator.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = coordinator.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = coordinator.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = coordinator.Email;
            param[9] = new MySqlParameter("@coordinatorId", MySqlDbType.VarChar);
            param[9].Value = coordinator.coordinatorId;

            rowsAffected = _DB.NonQuerySql(query, param);


            return rowsAffected;
        }
        public static int VerifyLogon(string username, string password)
        {
            string Username = "";
            string Password = "";
            bool isAdmin = true;
            int result = 0;


            SQLHelper _DB = new SQLHelper("bitconnString");

            string strQuery = "SELECT CoordinatorId, Username, Password, IsAdmin FROM coordinator WHERE Username = '" + username + "' AND Password = '" + password + " '";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);
            foreach (DataRow dr in dt.Rows)
            {
                Username = dr[1].ToString();
                Password = dr[2].ToString();
                isAdmin = Convert.ToBoolean(dr[3]);
            }

            if (username == Username && password == Password)
            {
                if (isAdmin == false)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
            }
            else
            {
                result = 2;
            }
            return result;
        }
    }
}
