using BIT_Service_Ver2.Model;
using BIT_Service_Ver2.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

            dgavailableContractors.DataContext = new JobAssignmentVM();
           
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            if (dgUnassigend.SelectedIndex >= 0)
            {

                int skill = int.Parse(txtSkillId.Text);

                dgavailableContractors.DataContext = new JobAssignmentVM(txtpreferredTime.Text, bookingDate.SelectedDate.Value, txtSuburb.Text, skill);
            }
            else
            {
                MessageBox.Show("Please make sure to select a job to assign.");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            List<DatePicker> dtPickerList;
            dtPickerList = GridSearch.Children.OfType<DatePicker>().ToList();
            foreach(DatePicker dt in dtPickerList)
            {
                dt.SelectedDate = null;
            }
            List<TextBox> txtList;
            txtList = GridSearch.Children.OfType<TextBox>().ToList();
            foreach (TextBox txt in txtList)
            {
                txt.Text = "";
            }
            List<TextBox> txtList1;
            txtList1 = MoreDets.Children.OfType<TextBox>().ToList();
            foreach (TextBox txt in txtList1)
            {
                txt.Text = "";
            }
        }

        private void BtnAssign_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = int.Parse(txtBookingId.Text);
            int clientId = int.Parse(txtClientId.Text);

            int rowsAffected = 0;

          
          foreach(var items in dgavailableContractors.ItemsSource)
            {
                ContractorAvailable contractor = items as ContractorAvailable;

                if(contractor.isChecked == true){

                    rowsAffected = JobAssignmentDB.insertAssignBooking(bookingId, clientId, contractor.contractorId);
                }
                
   
            }

            if (rowsAffected != 0)
            {
                MessageBox.Show("Job successfully assigned!");
            }
            else
            {
                MessageBox.Show("Assign Failed! Please make sure that you've selected contractor(s).");
            }

        }
    }
}
