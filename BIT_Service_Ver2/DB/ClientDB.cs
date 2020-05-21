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
    class ClientDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        
        public static ObservableCollection<Client> GetAllClients()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            //Get all Client Details
            string strQuery = "select clientId, FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password from Client";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn);

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt); //execute the query 

            //display all data of client table from the database
            var temp = new ObservableCollection<Client>();
            foreach (DataRow dr in dt.Rows)
            {
                Client client = new Client()
                {
                    clientID = Convert.ToInt32(dr[0]),
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
                temp.Add(client);
            }
            return temp;
        }

        //CRUD PART OF CLIENTS

        //INSERT CLIENT DETAILS
        public static int insertClient(Client client)
        {
            int rowsaffected;

            //insert all data of the new client
            string query = "INSERT INTO client (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password)" +
               " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email, @username, @password)";

            //parameter to be executed
            MySqlParameter[] param = new MySqlParameter[11];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = client.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = client.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = client.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = client.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = client.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = client.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = client.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = client.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = client.Email;
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = client.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = client.Password;

            rowsaffected = _DB.NonQuerySql(query, param); //execute query and params

            return rowsaffected;
        }
    
        //UPDATE CLIENT DETAILS
        public static int updateClient(Client client)
        {
            int rowsAffected;

            //update all data of the selected client
            string query = "UPDATE CLIENT SET FirstName = @firstName, SurName = @surName, DOB = @dob, Street = @street, " +
                "Suburb = @suburb, State = @state, PostCode = @postCode, Email = @email, MobileNumber = @mobileNumber, Username = @username, Password = @password WHERE ClientId = @clientID";

            //parameter to be executed
            MySqlParameter[] param = new MySqlParameter[12];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = client.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = client.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = client.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = client.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = client.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = client.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = client.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = client.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = client.Email;
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = client.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = client.Password;
            param[11] = new MySqlParameter("@clientID", MySqlDbType.Int32);
            param[11].Value = client.clientID;

            rowsAffected = _DB.NonQuerySql(query, param); //execute query and params


            return rowsAffected;

        }

        //READ SEPECIFIC CLIENT DETAILS
        public static ObservableCollection<Client> GetClient()
        {
            //Get only clientId, firstname and surname of the client from the client table
            string strQuery = "select clientId, FirstName, SurName from Client";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute the query

            //display the executed query
            var temp = new ObservableCollection<Client>();
            foreach (DataRow dr in dt.Rows)
            {
                Client client = new Client()
                {
                    clientID = Convert.ToInt32(dr[0]),
                    FirstName = dr[1].ToString(),
                    SurName = dr[2].ToString()
                };
                temp.Add(client);
            }
            return temp;
        }

        //SEARCH SPECIFIC CLIENT
        public static ObservableCollection<Client> SearchClient(string firstname)
        {
            //get all the details of the searched name of the client
            string strQuery = "SELECT * FROM client WHERE client.FirstName LIKE '%" + firstname + "%';";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute the query

            //display all data of the searched client
            var temp = new ObservableCollection<Client>();
            foreach (DataRow dr in dt.Rows)
            {
                Client client = new Client()
                {
                    clientID = Convert.ToInt32(dr[0]),
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
                temp.Add(client);
            }
            return temp;
        }
    }
}
