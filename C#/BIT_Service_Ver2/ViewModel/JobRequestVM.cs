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
        private int rowsAffected;
        private string _input;

        //Will be binded for the SearchInput from the UC: Search
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        //A list of all the job requests sent by the clients 
        public ObservableCollection<JobRequest> JobRequests
        {
            get { return _job; }
            set { _job = value; }
        }
        //A list of all the clients and their details
        public ObservableCollection<Client> Clients
        {
            get { return _client; }
            set { _client = value; }
        }
        //A list of all the skills and their details
        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        //A list of all the contractors and their details
        public ObservableCollection<Contractor> BookingContractors
        {
            get { return _contractors; }
            set { _contractors = value; }
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

        //constructor
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

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
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

        //This method will add a new row to the booking data grid
        private void AddBooking()
        {
            int lastRow = JobRequests.Count;
            JobRequest jobRequest = new JobRequest();
            DateTime dt = DateTime.Now;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    JobRequests.Add(jobRequest);

                }
            }
        }

        //Method for searching a specific booking information
        private void SearchBooking()
        {
            JobRequests.Clear();
            var temp = JobRequestDB.SearchJob(Input);
            foreach (var item in temp)
            {
                JobRequests.Add(item);
            }
        }

        //Method for adding a booking
        //Will be binded to the the 'Save' button command above
        private void InsertBooking()
        {
            try
            {
                if (SelectedJob == null)
                {
                    MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    //Method for validating the user input for the booking details
                    switch (SelectedJob.ValidateJobRequest())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = JobRequestDB.insertBooking(SelectedJob);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Booking added! You can now see it in the Job Assignment tab!", "Insert", MessageBoxButton.OK, MessageBoxImage.Question);
                            }
                            break;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Insert failed! Please make sure that you've given the necessary details to be added, please try again.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Method for updating a booking
        //Will be binded to the the 'Update' button command above
        private void UpdateBooking()
        {
            try
            {
                if (SelectedJob == null)
                {
                    MessageBox.Show("Please make sure that you've selected a job to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    //Method for validating the user input for the booking details
                    switch (SelectedJob.ValidateJobRequest())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = JobRequestDB.updateBooking(SelectedJob);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("booking updated!");
                            }
                            else
                            {
                                MessageBox.Show("update failed!");
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
