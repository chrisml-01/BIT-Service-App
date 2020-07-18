using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    public class DAL
    {
        MySqlConnection conn;
        private void ConnectDb()
        {
            string connStr = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;
            conn = new MySqlConnection(connStr);
        }
        public void Update(string queryStr)
        {
            ConnectDb();
            MySqlCommand updateCmd = new MySqlCommand(queryStr, conn);
            updateCmd.Connection.Open();
            updateCmd.ExecuteNonQuery();
            updateCmd.Connection.Close();
        }

        public void Add(string queryStr)
        {
            ConnectDb();
            MySqlCommand addCmd = new MySqlCommand(queryStr, conn);
            addCmd.Connection.Open();
            addCmd.ExecuteNonQuery();
            addCmd.Connection.Close();
        }

        public void Delete(string queryStr)
        {
            ConnectDb();
            MySqlCommand deleteCmd = new MySqlCommand(queryStr, conn);
            deleteCmd.Connection.Open();
            deleteCmd.ExecuteNonQuery();
            deleteCmd.Connection.Close();
        }

        public DataTable ReadRecords(string queryStr)
        {
            // open connection
            ConnectDb();
            DataTable dt = new DataTable();
            try
            {
                // build query object and adapter
                MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                // fill data table with query result and return

                adapt.Fill(dt);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dt;
        }
    }
}
