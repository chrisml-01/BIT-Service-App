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

        //A list of all the completed jobs and their details
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

        //constructor
        public JobApprovalVM()
        {
            var temp = JobRequestDB.GetAllJobApproval();
            foreach (var item in temp)
            {
                CompletedJob.Add(item);
            }
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
        public RelayCommand Approve
        {
            get { return new RelayCommand(ApproveBooking, true); }
        }

        public RelayCommand Disapprove
        {
            get { return new RelayCommand(DisapproveBooking, true); }
        }

        //Method for approving a completed job
        //This will be binded to the 'Approve' button commands above.
        private void ApproveBooking()
        {
            if(SelectedJob == null)
            {
                MessageBox.Show("Please make sure that you've selected a job to approve");
            }
            else
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
           

        }

        //Method for disapproving a completed job
        //This will be binded to the 'Disapprove' button commands above.
        private void DisapproveBooking()
        {
            if (SelectedJob == null)
            {
                MessageBox.Show("Please make sure that you've selected a job to approve");
            }
            else
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
}
