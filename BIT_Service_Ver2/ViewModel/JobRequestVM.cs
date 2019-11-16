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

        public RelayCommand Add
        {
            get { return new RelayCommand(InsertBooking, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateBooking, true); }
        }

        private void InsertBooking()
        {

            rowsAffected = JobRequestDB.insertBooking(SelectedJob);

            if (rowsAffected != 0)
            {
                MessageBox.Show("booking added!");
            }
            else
            {
                MessageBox.Show("insert failed!");
            }

        }

        private void UpdateBooking()
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
        //public JobRequestVM(int index)
        //{
        //    var temp = JobRequestDB.GetAllJob(index);
        //    foreach (var item in temp)
        //    {
        //        JobRequests.Add(item);
        //    }
        //}
        //public JobRequestVM(string bookingId)
        //{
        //    var contractors = ContractorDB.GetAllBookingContractors(bookingId);
        //    foreach (var item in contractors)
        //    {
        //        BookingContractors.Add(item);
        //    }
        //}





    }
}
