using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BIT_Service_Ver2.ViewModel
{
    class AvailabilityVM : NotifyClass
    {
        //variables
        private ObservableCollection<TimeSlot> _timeSlot = new ObservableCollection<TimeSlot>();
        private ObservableCollection<Contractor> _contractors = new ObservableCollection<Contractor>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private ObservableCollection<Days> _days = new ObservableCollection<Days>();
        private Contractor _selectedContractor;
        private int rowsAffected;
        
        //A list of all contractors saved in the database with their details
        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set { _contractors = value; }
        }
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;
                //After selecting a specific contractor the two methods (checkSkills() & getAvail()) below will be used
                //to check or match their skills and available days to the existing list of skills and days within the database
                checkSkills();
                getAvail();
                OnPropertyChanged("SelectedContractor");               

            }
        }

        //A list of all the days saved within the database
        public ObservableCollection<Days> Days
        {
            get { return _days; }
            set { _days = value; }
        }
        //A list of all the skills saved within the database
        public ObservableCollection<Skill> Skill
        {
            get { return _skill; }
            set { _skill = value; }
        }
        //A list of all the time slots saved within the database
        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return _timeSlot; }
            set { _timeSlot = value; }
        }
        
        //constructor
        public AvailabilityVM()
        {
            var contractors = ContractorDB.GetAllContractors();
            foreach (var item in contractors)
            {
                Contractors.Add(item);
            }

            var days = AvailabilityDB.GetAllDays();
            foreach (var item in days)
            {
                Days.Add(item);
            }

            var skills = SkillDB.GetAllSkills();
            foreach(var item in skills)
            {
                Skill.Add(item);
            }

            var time = AvailabilityDB.GetAllTime();
            foreach (var item in time)
            {
                TimeSlots.Add(item);
            }

        }

        //Method for getting all availabilities of contractors
        //This method will match all the days of the week with the available days of the selected contractor
        public void getAvail()
        {
            foreach(Days days in Days)
            {
                days.isChecked = false;
                foreach(Days avail in AvailabilityDB.GetAllAvailability(SelectedContractor))
                {
                    if (avail.dayId == days.dayId)
                    {
                        days.isChecked = true;
                    }
                }   
            }          
        }

        //Method for getting all the skills of contractors
        //This method will match the skills of a contractor with all the skills saved in the database
        public void checkSkills()
        {
            foreach(Skill contractors in Skill)
            {
                contractors.isChecked = false;
                foreach(Skill skill in AvailabilityDB.GetAllContractorSkills(SelectedContractor))
                {
                    
                    if (skill.skillID == contractors.skillID)
                    {
                        contractors.isChecked = true;
                    }
                }
            }
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Control): Buttons
        public RelayCommand AddDays
        {
            get { return new RelayCommand(insertDays, true); }
        }

        public RelayCommand AddSkills
        {
            get { return new RelayCommand(insertSkill, true); }
        }

        public RelayCommand UpdateDays
        {
            get { return new RelayCommand(updateDays, true); }
        }

        public RelayCommand UpdateSkills
        {
            get { return new RelayCommand(updateSkill, true); }
        }

        //Method for inserting a new contractor skill
        //This will be binded to the 'AddSkills' button command above.
        private void insertSkill()
        {          
            foreach(Skill s in Skill)
            {                   
                if(s.isChecked == true)
                {
                   rowsAffected = AvailabilityDB.insertSkills(SelectedContractor.contractorID, s.skillID);
                }                   
            }
           
            if (rowsAffected != 0)
            {
                MessageBox.Show("Contractor skill added!");
            }
            else
            {
                MessageBox.Show("insert failed!");
            }
        }

        //Method for updating a contractor skill
        //This will be binded to the 'UpdateSkills' button command above.
        private void updateSkill()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please make sure that you've selected a contractor to update.");
            }
            else
            {
                int delete = AvailabilityDB.deleteSkills(SelectedContractor.contractorID);

                foreach (Skill s in Skill)
                {
                    if (s.isChecked == true)
                    {
                        rowsAffected = AvailabilityDB.insertSkills(SelectedContractor.contractorID, s.skillID);
                    }

                }

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Contractor skill updated!");
                }
                else
                {
                    MessageBox.Show("update failed!");
                }
            }
        }

        //Method for adding new contractor available day/s 
        //This method will be binded the 'AddDays' button command above.
        private void insertDays()
        {
            foreach(Days d in Days)
            {
                if(d.isChecked == true)
                {
                    rowsAffected = AvailabilityDB.insertDays(SelectedContractor.contractorID, d.dayId);
                }
            }

            if (rowsAffected != 0)
            {
                MessageBox.Show("Availability inserted!");
            }
            else
            {
                MessageBox.Show("insert failed!");
            }
        }

        //Method for updating contractor available day/s 
        //This method will be binded the 'UpdateDays' button command above.
        private void updateDays()
        {
            if(SelectedContractor == null)
            {
                MessageBox.Show("Please make sure that you've selected a contractor to update");
            }
            else
            {
                int delete = AvailabilityDB.deleteDays(SelectedContractor.contractorID);

                foreach (Days d in Days)
                {
                    if (d.isChecked == true)
                    {
                        rowsAffected = AvailabilityDB.insertDays(SelectedContractor.contractorID, d.dayId);
                    }

                }

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Contractor availability updated!");
                }
                else
                {
                    MessageBox.Show("update failed!");
                }
            }            
        }
    }
}
