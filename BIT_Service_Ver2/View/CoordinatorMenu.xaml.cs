using System;
using System.Collections.Generic;
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
    /// Interaction logic for CoordinatorMenu.xaml
    /// </summary>
    public partial class CoordinatorMenu : Window
    {
        public CoordinatorMenu()
        {
            InitializeComponent();
        }

        private void BtnCoordinator_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StaffManagement();
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientManagement();
        }

        private void BtnContractor_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ContractorManagement();
        }

        private void BtnJobManagement_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobManagement();
        }

        private void BtnJobAssigment_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobAssignment();
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            
            btnClient.Visibility = Visibility.Collapsed;
            btnContractor.Visibility = Visibility.Collapsed;
            btnJobAssigment.Visibility = Visibility.Collapsed;
            btnJobManagement.Visibility = Visibility.Collapsed;
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientManagement();
            btnClient.Visibility = Visibility.Visible;
            btnContractor.Visibility = Visibility.Visible;            
            btnJobAssigment.Visibility = Visibility.Collapsed;
            btnJobManagement.Visibility = Visibility.Collapsed;
            
        }

        private void BtnJob_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobManagement();
            btnJobAssigment.Visibility = Visibility.Visible;
            btnJobManagement.Visibility = Visibility.Visible;
            
            btnClient.Visibility = Visibility.Collapsed;
            btnContractor.Visibility = Visibility.Collapsed;
            
        }

        private void BtnSkill_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SkillManagement();   
        }
    }
}
