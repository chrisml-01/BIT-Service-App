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
    class JobRequestVM : NotifyClass
    {
        private ObservableCollection<JobRequest> _job = new ObservableCollection<JobRequest>();
        private ObservableCollection<Client> _client = new ObservableCollection<Client>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private ObservableCollection<Contractor> _contractors = new ObservableCollection<Contractor>();
        private JobRequest _selectedJob;
        private Skill _selectedSkill;
        private Client _selectedClient;
        private int rowsAffected;
        private string _input;
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        public  ObservableCollection<JobRequest> JobRequests
        {
            get { return _job; }
            set { _job = value; }
        }

        public ObservableCollection<Client> Clients
        {
            get { return _client; }
            set { _client = value; }
        }

        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public ObservableCollection<Contractor> BookingContractors
        {
            get { return _contractors; }
            set { _contractors = value; }
        }

        public JobRequest SelectedJob
        {
            get { return _selectedJob; }
            set { _selectedJob = value;
                OnPropertyChanged("SelectedJob");
            }
        }

        public Skill SelectedSkill
        {
            get { return _selectedSkill ; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedJob");
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedJob");
            }
        }

        public JobRequestVM()
        {
            var temp = JobRequestDB.GetAllJob();
            foreach (var item in temp)
            {
                JobRequests.Add(item);
            }

            var client = ClientDB.GetClient();
            foreach (var item in client)
            {
                Clients.Add(item);
            }

            var skills = SkillDB.GetAllSkills();
            foreach (var item in skills)
            {
                Skills.Add(item);
            }
        }

        public RelayCommand Save
        {
            get { return new RelayCommand(InsertBooking, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateBooking, true); }
        }

        public RelayCommand Search
        {
            get { return new RelayCommand(SearchBooking, true); }
        }

        public RelayCommand Add
        {
            get { return new RelayCommand(AddBooking, true); }
        }

        private void AddBooking()
        {
            int lastRow = JobRequests.Count;
            JobRequest jobRequest = new JobRequest();

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    JobRequests.Add(jobRequest);
                }
            }

        }

        private void SearchBooking()
        {
            JobRequests.Clear();
            var temp = JobRequestDB.SearchJob(Input);
            foreach (var item in temp)
            {
                JobRequests.Add(item);
            }
        }

        private void InsertBooking()
        {
            if(SelectedJob == null)
            {
                MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }else if(ValidateBooking(SelectedJob) == 0)
            {

            }else if(ValidateBooking(SelectedJob) == 1)
            {
                rowsAffected = JobRequestDB.insertBooking(SelectedJob);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Booking added! You can now see it in the Job Assignment tab!","Insert", MessageBoxButton.OK,MessageBoxImage.Question);
                }
                else
                {
                    MessageBox.Show("insert failed!");
                }
            }
            else
            {
                MessageBox.Show("Error");
            }


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

        private static int ValidateBooking(JobRequest job)
        {
            int result = 0;
            

            if(job.bookingDate < DateTime.Now)
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
