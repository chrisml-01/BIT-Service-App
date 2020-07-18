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
    class ContractorDB
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        //READ ALL CONTRACTOR DETAILS
        public static ObservableCollection<Contractor> GetAllContractors()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bitconnString"].ConnectionString;

            MySqlConnection myConn = new MySqlConnection(connectionString);

            //Get all Contractor Details
            string strQuery = "select contractorId, FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password from Contractor";
            MySqlCommand cmd = new MySqlCommand(strQuery, myConn); //execute query

            DataTable dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);  //execute query

            //display all data of the executed query
            var temp = new ObservableCollection<Contractor>();
            foreach (DataRow dr in dt.Rows)
            {
                Contractor contractor = new Contractor()
                {
                    contractorID = Convert.ToInt32(dr[0]),
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
                temp.Add(contractor);
            }
            return temp;
        }

        //CRUD PART OF CONTRACTORS

        //INSERT CONTRACTOR DETAILS
        public static int insertContractor(Contractor contractor)
        {
            int rowsaffected;

            //insert all the details of new contractor 
            string query = "INSERT INTO contractor (FirstName, SurName, DOB, Street, Suburb, State, Postcode, MobileNumber, Email, Username, Password)" +
               " VALUES (@firstName, @surName, @dob, @street, @suburb, @state, @postcode, @mobileNumber, @email, @username, @password)";

            //parameters to be executed
            MySqlParameter[] param = new MySqlParameter[11];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = contractor.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = contractor.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = contractor.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = contractor.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = contractor.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = contractor.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = contractor.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = contractor.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = contractor.Email;
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = contractor.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = contractor.Password;

            rowsaffected = _DB.NonQuerySql(query, param); //execute query and params

            return rowsaffected;
        }

        //UPDATE CONTRACTOR DETAILS
        public static int updateContractor(Contractor contractor)
        {
            int rowsAffected;

            //update all data of the selected contractor
            string query = "UPDATE CONTRACTOR SET FirstName = @firstName, SurName = @surName, DOB = @dob, Street = @street, " +
                "Suburb = @suburb, State = @state, PostCode = @postCode, Email = @email, MobileNumber = @mobileNumber, Username = @username, Password = @password WHERE ContractorId = @contractorId";

            //parameters to be executed
            MySqlParameter[] param = new MySqlParameter[12];
            param[0] = new MySqlParameter("@firstname", MySqlDbType.VarChar);
            param[0].Value = contractor.FirstName;
            param[1] = new MySqlParameter("@surName", MySqlDbType.VarChar);
            param[1].Value = contractor.SurName;
            param[2] = new MySqlParameter("@dob", MySqlDbType.Date);
            param[2].Value = contractor.DOB;
            param[3] = new MySqlParameter("@street", MySqlDbType.VarChar);
            param[3].Value = contractor.Street;
            param[4] = new MySqlParameter("@suburb", MySqlDbType.VarChar);
            param[4].Value = contractor.Suburb;
            param[5] = new MySqlParameter("@state", MySqlDbType.VarChar);
            param[5].Value = contractor.State;
            param[6] = new MySqlParameter("@postcode", MySqlDbType.VarChar);
            param[6].Value = contractor.Postcode;
            param[7] = new MySqlParameter("@mobileNumber", MySqlDbType.VarChar);
            param[7].Value = contractor.MobileNum;
            param[8] = new MySqlParameter("@email", MySqlDbType.VarChar);
            param[8].Value = contractor.Email;
            param[9] = new MySqlParameter("@username", MySqlDbType.VarChar);
            param[9].Value = contractor.Username;
            param[10] = new MySqlParameter("@password", MySqlDbType.VarChar);
            param[10].Value = contractor.Password;
            param[11] = new MySqlParameter("@contractorId", MySqlDbType.VarChar);
            param[11].Value = contractor.contractorID;

            rowsAffected = _DB.NonQuerySql(query, param); //execute query and params


            return rowsAffected;

        }

        //SEARCH SPECIFIC CONTRACTOR
        public static ObservableCollection<Contractor> SearchContractor(string firstname)
        {
            //get all the details of the searched name of the contractor
            string strQuery = "SELECT * FROM contractor WHERE contractor.FirstName LIKE '%" + firstname + "%'";
            
            DataTable dt = new DataTable();

            dt = _DB.executeSQL(strQuery); //execute query

            //display all data of the searched contractor
            var temp = new ObservableCollection<Contractor>();
            foreach (DataRow dr in dt.Rows)
            {
                Contractor contractor = new Contractor()
                {
                    contractorID = Convert.ToInt32(dr[0]),
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
                temp.Add(contractor);
            }
            return temp;
        }

    }
}
