using BIT_Service_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BIT_Service_Ver2.Model
{
    class ContractorAvailable : NotifyClass
    {
        public bool isChecked { get; set; }
        public int contractorId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

    }
}
