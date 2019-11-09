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
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private Availability _selectedContractor;
        //private int rowsAffected;
        public ObservableCollection<Availability> Contractors
        {
            get { return _contractors; }
            set { _contractors = value; }
        }
        public Availability SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;
                OnPropertyChanged("SelectedContractor");
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

            var skills = SkillDB.GetAllSkills();
            foreach (var item in skills)
            {
                Skills.Add(item);
            }
        }
    }
}
