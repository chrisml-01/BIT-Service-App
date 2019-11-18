using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_Service_Ver2.ViewModel
{
    class JobApprovalVM:NotifyClass
    {
        private ObservableCollection<JobApproval> _job = new ObservableCollection<JobApproval>();
        private JobApproval _selectedJob;
        private int rowsAffected;

        public ObservableCollection<JobApproval> CompletedJob
        {
            get { return _job; }
            set { _job = value; }
        }

        public JobApproval SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                OnPropertyChanged("SelectedJob");
            }
        }

        public JobApprovalVM()
        {
            var temp = JobRequestDB.GetAllJobApproval();
            foreach (var item in temp)
            {
                CompletedJob.Add(item);
            }
        }

        public RelayCommand Approve
        {
            get { return new RelayCommand(ApproveBooking, true); }
        }

        public RelayCommand Disapprove
        {
            get { return new RelayCommand(DisapproveBooking, true); }
        }

        private void ApproveBooking()
        {

            rowsAffected = JobRequestDB.approveBooking(SelectedJob);

            if (rowsAffected != 0)
            {
                MessageBox.Show("booking approved!");
            }
            else
            {
                MessageBox.Show("update failed!");
            }

        }

        private void DisapproveBooking()
        {

            rowsAffected = JobRequestDB.disapproveBooking(SelectedJob);

            if (rowsAffected != 0)
            {
                MessageBox.Show("booking disapproved!");
            }
            else
            {
                MessageBox.Show("update failed!");
            }

        }
    }
}
