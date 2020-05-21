using BIT_Service_Ver2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Logger
{
    public class DBLogger : LogBase
    {
        private static SQLHelper _DB = new SQLHelper("bitconnString");

        public override int Log(string className, string message)
        {
            lock (lockObj)
            {
                int rowsAffected;

                string query = "CALL usp_insertErrorLog(@class, @message)";

                MySqlParameter[] param = new MySqlParameter[2];
                param[0] = new MySqlParameter("@class", MySqlDbType.VarChar);
                param[0].Value = className;
                param[1] = new MySqlParameter("@message", MySqlDbType.VarChar);
                param[1].Value = message;

                rowsAffected = _DB.NonQuerySql(query, param);

                return rowsAffected;
            }
        }

        public override void Log(string message)
        {
            throw new NotImplementedException();
        }
    }
}
