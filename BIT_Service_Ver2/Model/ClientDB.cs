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
    class ClientDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        //Get all Client Details
        public static ObservableCollection<Client> GetAllClients()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            string strQuery = "select clientId, FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password from Client";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn);

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);

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

        public static int insertClient(Client client)
        {
            int rowsaffected;

            string query = "INSERT INTO client (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email)" +
               " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email)";

            Client Addclient = new Client();
            MySqlParameter[] param = new MySqlParameter[9];
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

            rowsaffected = _DB.NonQuerySql(query, param);

            return rowsaffected;
        }

        public static int updateClient(Client client)
        {
            int rowsAffected;

            string query = "UPDATE CLIENT SET FirstName = @firstName, SurName = @surName, DOB = @dob, Street = @street, " +
                "Suburb = @suburb, State = @state, PostCode = @postCode, Email = @email, MobileNumber = @mobileNumber WHERE ClientId = @clientID";

            Client Addclient = new Client();
            MySqlParameter[] param = new MySqlParameter[10];
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
            param[9] = new MySqlParameter("@clientID", MySqlDbType.Int32);
            param[9].Value = client.clientID;

            rowsAffected = _DB.NonQuerySql(query, param);


            return rowsAffected;

        }

        public static ObservableCollection<Client> GetClient()
        {

            string strQuery = "select clientId, FirstName, SurName from Client";

            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery);

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
    }
}