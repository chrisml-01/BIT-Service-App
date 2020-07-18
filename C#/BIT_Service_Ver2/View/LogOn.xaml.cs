using BIT_Service_Ver2.Logger;
using BIT_Service_Ver2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIT_Service_Ver2.View
{
    /// <summary>
    /// Interaction logic for LogOn.xaml
    /// </summary>
    public partial class LogOn : Window
    {
        public LogOn()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string un = txtUsername.Text;
                string pwd = txtPassword.Password;

                int result = VerifyLogon(un, pwd);

                if (result == 0)
                {
                    MessageBox.Show("Welcome back, " + un);
                    this.Hide();
                    CoordinatorMenu main = new CoordinatorMenu();
                    main.ShowDialog();
                    this.Close();
                }
                else if (result == 1)
                {
                    MessageBox.Show("Welcome back, Admin " + un);
                    this.Hide();
                    MainWindow main = new MainWindow();
                    main.txtName.Text = un;
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Password or Username, Please Try Again");
                    txtUsername.Focus();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LogHelper.Log(LogTarget.File, "LogOn", ex.ToString());
            }

        }
        //Method to verify the username and password logon 
        //This method connects to the database and retrieves the ID, Username, Password and wether they're an admin or not
        public static int VerifyLogon(string username, string password)
        {
            string Username = "";
            string Password = "";
            bool isAdmin = true;

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
                if (!isAdmin)
                {
                    return 0;
                }
                return 1;
            }

            return 2;
          
        }
    }
}
