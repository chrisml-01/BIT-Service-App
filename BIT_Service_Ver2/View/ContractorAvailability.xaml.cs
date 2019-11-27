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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIT_Service_Ver2.View
{
    /// <summary>
    /// Interaction logic for ContractorAvailability.xaml
    /// </summary>
    public partial class ContractorAvailability : Window
    {
        public ContractorAvailability()
        {
            InitializeComponent();
            DataContext = new AvailabilityVM();
        }

      
    }
}
