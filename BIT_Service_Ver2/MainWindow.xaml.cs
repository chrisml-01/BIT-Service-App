using BIT_Service_Ver2.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIT_Service_Ver2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientManagement();
            btnCoordinator.Visibility = Visibility.Visible;
            btnClient.Visibility = Visibility.Visible;
            btnContractor.Visibility = Visibility.Visible;
            btnJobAssigment.Visibility = Visibility.Collapsed;
            btnJobManagement.Visibility = Visibility.Collapsed;
            btnSkill.Visibility = Visibility.Collapsed;
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientManagement();
        }

        private void BtnContractor_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ContractorManagement();
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            btnCoordinator.Visibility = Visibility.Collapsed;
            btnClient.Visibility = Visibility.Collapsed;
            btnContractor.Visibility = Visibility.Collapsed;
            btnJobAssigment.Visibility = Visibility.Collapsed;
            btnJobManagement.Visibility = Visibility.Collapsed;
            btnSkill.Visibility = Visibility.Collapsed;
        }

        private void BtnServices_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobManagement();
            btnJobAssigment.Visibility = Visibility.Visible;
            btnJobManagement.Visibility = Visibility.Visible;
            btnSkill.Visibility = Visibility.Visible;
            btnCoordinator.Visibility = Visibility.Collapsed;
            btnClient.Visibility = Visibility.Collapsed;
            btnContractor.Visibility = Visibility.Collapsed;
           
        }

        private void BtnJobManagement_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobManagement();
        }

        private void BtnJobAssigment_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new JobAssignment();
        }

        private void BtnSkill_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SkillManagement();
        }

        private void BtnCoordinator_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StaffManagement();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //Window logOn = new LogOn();
            //logOn.ShowDialog();
        }
    }
}
