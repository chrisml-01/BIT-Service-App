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

        //READ ALL COORDINATOR DETAILS
        public static ObservableCollection<Coordinator> GetAllCoordinators()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            //Get all Coordinator Details
            string strQuery = "select coordinatorId, FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password from Coordinator";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn); //execute the query

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt); //execute the query

            //display the all the data of the executed query (display all data of the coordinators)
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

        //CRUD PART OF COORDINATOR
        //INSERT COORDINATOR DETAILS
        public static int insertCoordinator(Coordinator coordinator)
        {
            int rowsaffected;
            //insert all the details of the new coordinator
            string query = "INSERT INTO coordinator (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password)" +
               " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email, @username, @password)";

            //parameters to be executed along with the query above
            MySqlParameter[] param = new MySqlParameter[11];
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
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = coordinator.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = coordinator.Password;

            rowsaffected = _DB.NonQuerySql(query, param); //execute the query and params

            return rowsaffected;
        }

        //UPDATE COORDINATOR DETAILS
        public static int updateCoordinator(Coordinator coordinator)
        {
            int rowsAffected;

            //update the selected coordinator details
            string query = "UPDATE COORDINATOR SET FirstName = @firstName, SurName = @surName, DOB = @dob, Street = @street, " +
                "Suburb = @suburb, State = @state, PostCode = @postCode, Email = @email, MobileNumber = @mobileNumber, Username = @username, Password = @password WHERE CoordinatorId = @coordinatorId";

            //parameters to be executed along with the query above
            MySqlParameter[] param = new MySqlParameter[12];
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
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = coordinator.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = coordinator.Password;
            param[11] = new MySqlParameter("@coordinatorId", MySqlDbType.VarChar);
            param[11].Value = coordinator.coordinatorId;

            rowsAffected = _DB.NonQuerySql(query, param); //execute the query and params


            return rowsAffected;
        }

        //SEARCH SPECIFIC COORDINATOR
        public static ObservableCollection<Coordinator> SearchCoordinator(string firstname)
        {
            //select all details of the searched coordinator 
            string strQuery = "SELECT * FROM coordinator WHERE coordinator.FirstName LIKE '%" + firstname+ "%'";
           
            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute query

            //display all details of the searched coordinator
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

        public static ObservableCollection<Coordinator> GetAllCoords()
        {
            //select all details of the searched coordinator 
            string strQuery = "SELECT CoordinatorId, CONCAT(FirstName, ' ', SurName) FROM coordinator";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute query

            //display all details of the searched coordinator
            var temp = new ObservableCollection<Coordinator>();
            foreach (DataRow dr in dt.Rows)
            {
                Coordinator coordinator = new Coordinator()
                {
                    coordinatorId = Convert.ToInt32(dr[0]),
                    coordinatorName = dr[1].ToString()
                    
                };
                temp.Add(coordinator);
            }
            return temp;
        }

    }
}
