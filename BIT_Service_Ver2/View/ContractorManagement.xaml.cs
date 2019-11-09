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
    /// Interaction logic for ContractorManagement.xaml
    /// </summary>
    public partial class ContractorManagement : Page
    {
        public ContractorManagement()
        {
            InitializeComponent();
            DataContext = new ContractorViewModel();
        }

        private void BtnAvailability_Click(object sender, RoutedEventArgs e)
        {
            Window avail = new ContractorAvailability();
            avail.ShowDialog();

        }
    }
}
