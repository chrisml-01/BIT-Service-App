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
        private ObservableCollection<Availability> _contractorAvail = new ObservableCollection<Availability>();
        private ObservableCollection<ContractorSkills> _contractorSkills = new ObservableCollection<ContractorSkills>();
        private ObservableCollection<PreferredLocation> _contractorLocation = new ObservableCollection<PreferredLocation>();
        private ObservableCollection<TimeSlot> _timeSlot = new ObservableCollection<TimeSlot>();
        private ObservableCollection<Contractor> _contractors = new ObservableCollection<Contractor>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private ObservableCollection<Days> _days = new ObservableCollection<Days>();
        private Contractor _selectedContractor;
        private int rowsAffected;
        private int tab;
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
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
                checkSkills();
                getAvail();
                OnPropertyChanged("SelectedContractor");               

            }
        }

        public ObservableCollection<Days> Days
        {
            get { return _days; }
            set { _days = value; }
        }

        public ObservableCollection<Skill> Skill
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return _timeSlot; }
            set { _timeSlot = value; }
        }

        public ObservableCollection<Skill> Skills
        {

            get
            {
                if (_selectedContractor != null)
                {
                    _selectedContractor.skills = SkillDB.GetAllSkills();
                    return _selectedContractor.skills;
                }
                return null;
            }
            set
            {
                _selectedContractor.skills = value;
                _selectedContractor.skills = SkillDB.GetAllSkills();
                OnPropertyChanged("SelectedContractor");
            }
        }

        public ObservableCollection<Days> Availabilities
        {
            get
            {
                if(_selectedContractor != null)
                {
                    _selectedContractor.availabilities = AvailabilityDB.GetAllDays();
                    return _selectedContractor.availabilities;
                }
                return null;
            }
            set
            {
                _selectedContractor.availabilities = value;
                _selectedContractor.availabilities = AvailabilityDB.GetAllDays();
                OnPropertyChanged("SelectedContractor");
            }
        }

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

        public void getAvail()
        {

            foreach(Days contractors in Days)
            {
                contractors.isChecked = false;
                foreach(Days avail in AvailabilityDB.GetAllAvailability(SelectedContractor))
                {
                    if (avail.dayId == contractors.dayId)
                    {
                        contractors.isChecked = true;
                    }
                }
            }          
        }

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

        private void updateSkill()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please make sure that you've selected a contractor to update");
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
