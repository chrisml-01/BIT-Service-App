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
    class JobAssignmentVM : NotifyClass
    {
        private ObservableCollection<JobRequest> _job = new ObservableCollection<JobRequest>();
        private ObservableCollection<ContractorAvailable> _contractors = new ObservableCollection<ContractorAvailable>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private JobRequest _selectedJob;
        private ContractorAvailable _selectedContractor;
        private int rowsAffected;

        public ObservableCollection<JobRequest> UnassignedJob
        {
            get { return _job; }
            set { _job = value; }
        }

        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public ObservableCollection<ContractorAvailable> ContractorsAvailable
        {
            get { return _contractors; }
            set { _contractors = value; }
        }

        public ContractorAvailable SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;
                OnPropertyChanged("SelectedContractor");
            }
        }

        public JobRequest SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                OnPropertyChanged("SelectedJob");
            }
        }


        public JobAssignmentVM()
        {

            var temp = JobRequestDB.GetAllUnassigned();
            foreach (var item in temp)
            {
                UnassignedJob.Add(item);
            }

            var skills = SkillDB.GetAllSkills();
            foreach (var item in skills)
            {
                Skills.Add(item);
            }
        }

        public JobAssignmentVM(string time, DateTime bookingdate, string suburb, int contractorSkill)
        {

            var contractors = JobAssignmentDB.GetAllAvailableContractors(time, bookingdate, suburb, contractorSkill);

            foreach (var item in contractors)
            {
                ContractorsAvailable.Add(item);
            }           
            
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateBooking, true); }
        }

        private void UpdateBooking()
        {

            if (SelectedJob == null)
            {
                MessageBox.Show("Please make sure that you've selected a job to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (ValidateBooking(SelectedJob) == 0)
            {

            }
            else if (ValidateBooking(SelectedJob) == 1)
            {
                rowsAffected = JobRequestDB.updateBooking(SelectedJob);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("booking updated!");
                }
                else
                {
                    MessageBox.Show("update failed!");
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        public void AssignBooking(int bookingId, int clientId, int contractorId)
        {
            rowsAffected = JobAssignmentDB.insertAssignBooking(bookingId, clientId, contractorId);

            if (rowsAffected != 0)
            {
                MessageBox.Show("client added!");
            }
            else
            {
                MessageBox.Show("insert failed!");
            }

        }

        private static int ValidateBooking(JobRequest job)
        {
            int result = 0;


            if (job.bookingDate < DateTime.Now)
            {
                MessageBox.Show("Booking date shouldn't be a date from the past. Please try again.");
                result = 0;
            }
            else if (job.street.Length > 180 && job.suburb.Length > 180 && job.notes.Length > 180)
            {
                MessageBox.Show("Please make sure that your input doesn't exceed 180 characters.");
                result = 0;
            }
            else if (job.postcode.Length > 4)
            {
                MessageBox.Show("Please make sure that your postcode doesn't exceed 4 characters.");
                result = 0;
            }
            else
            {
                result = 1;
            }

            return result;
        }

    }
}
