using BIT_Service_Ver2.ViewModel;
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

namespace BIT_Service_Ver2.View
{
    /// <summary>
    /// Interaction logic for JobManagement.xaml
    /// </summary>
    public partial class JobManagement : Page
    {
        public JobManagement()
        {
            InitializeComponent();
            DataContext = new JobRequestVM();

            //dgAll.DataContext = new JobRequestVM(cbxFilter.SelectedIndex);

            //int bookingId = int.Parse(txtBookingId.Text);

            //lstContractor.DataContext = new JobRequestVM(txtBookingId.Text);


        }
    }
}
