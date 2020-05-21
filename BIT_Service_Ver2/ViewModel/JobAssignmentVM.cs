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
        private ObservableCollection<Coordinator> _coordinators = new ObservableCollection<Coordinator>();
        private JobRequest _selectedJob;
        private ContractorAvailable _selectedContractor;
        private int rowsAffected;
       

        //A list of all the unassigned jobs and their details
        public ObservableCollection<JobRequest> UnassignedJob
        {
            get { return _job; }
            set { _job = value; }
        }

        //A list of all the skills and their details
        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        //A list of all the coordinators and their details
        public ObservableCollection<Coordinator> Coordinators
        {
            get { return _coordinators; }
            set { _coordinators = value; }
        }

        //A list of all the contractors available and their details
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

        //constructor
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

            var coord = CoordinatorDB.GetAllCoords();
            foreach (var item in coord)
            {
                Coordinators.Add(item);
            }
        }

        //constructor with parameters to search for available contractors of the selected job
        public JobAssignmentVM(string time, DateTime bookingdate, string suburb, int contractorSkill)
        {

            var contractors = JobAssignmentDB.GetAllAvailableContractors(time, bookingdate, suburb, contractorSkill);

            foreach (var item in contractors)
            {
                ContractorsAvailable.Add(item);
            }           
            
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateBooking, true); }
        }

        public RelayCommand Delete
        {
            get { return new RelayCommand(DeleteBooking, true); }
        }

        //Method for updating a booking
        //This will be binded to the 'Update' button command above
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

        //Method for deleting a booking
        //This will be binded to the 'Delete' button command above
        private void DeleteBooking()
        {
            try
            {
                if (SelectedJob == null)
                {
                    MessageBox.Show("Please make sure that you've selected a job to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (SelectedJob.ValidateJobRequest())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = JobRequestDB.DeleteBooking(SelectedJob);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("booking deleted!");
                            }
                            else
                            {
                                MessageBox.Show("delete failed!");
                            }
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        //Method for assigning a booking to contractor/s
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
    }
}
