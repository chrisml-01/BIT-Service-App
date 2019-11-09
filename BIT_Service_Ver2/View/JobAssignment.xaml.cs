using BIT_Service_Ver2.Model;
using BIT_Service_Ver2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIT_Service_Ver2.View
{
    /// <summary>
    /// Interaction logic for JobAssignment.xaml
    /// </summary>
    public partial class JobAssignment : Page
    {
        public JobAssignment()
        {
            InitializeComponent();
            DataContext = new JobAssignmentVM();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            int skill = int.Parse(txtSkillId.Text);

            dgavailableContractors.DataContext = new JobAssignmentVM(txtpreferredTime.Text, bookingDate.SelectedDate.Value, txtSuburb.Text, skill);
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
