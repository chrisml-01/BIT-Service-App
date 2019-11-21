using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.ViewModel
{
    class AvailabilityVM : NotifyClass
    {
        private ObservableCollection<Availability> _contractors = new ObservableCollection<Availability>();
        private ObservableCollection<ContractorSkills> _contractorSkills = new ObservableCollection<ContractorSkills>();
        private ObservableCollection<PreferredLocation> _contractorLocation = new ObservableCollection<PreferredLocation>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private ObservableCollection<Days> _days = new ObservableCollection<Days>();
        private Availability _selectedContractor;
        private ContractorSkills _selectedSkill;
        private PreferredLocation _selectedLocation;
        //private int rowsAffected;
        public ObservableCollection<Availability> Contractors
        {
            get { return _contractors; }
            set { _contractors = value; }
        }
        public ObservableCollection<Days> Days
        {
            get { return _days; }
            set { _days = value; }
        }
        public ObservableCollection<PreferredLocation> ContractorLocation
        {
            get { return _contractorLocation; }
            set { _contractorLocation = value; }
        }
        public ObservableCollection<ContractorSkills> ContractorSkill
        {
            get { return _contractorSkills; }
            set { _contractorSkills = value; }
        }
        public Availability SelectedAvail
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;
                OnPropertyChanged("SelectedAvail");
            }
        }
        public ContractorSkills SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }
        public PreferredLocation SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }
        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public AvailabilityVM()
        {
            var temp = AvailabilityDB.GetAllAvailability();
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }

            var con_skills = AvailabilityDB.GetAllContractorSkills();
            foreach (var item in con_skills)
            {
                ContractorSkill.Add(item);
            }

            var con_location = AvailabilityDB.GetAllPreferredLocation();
            foreach (var item in con_location)
            {
                ContractorLocation.Add(item);
            }

            var skills = SkillDB.GetAllSkills();
            foreach (var item in skills)
            {
                Skills.Add(item);
            }

            var days = AvailabilityDB.GetAllDays();
            foreach (var item in days)
            {
                Days.Add(item);
            }

        }
    }
}
