using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.ViewModel
{
    class AvailabilityVM : NotifyClass
    {
        private ObservableCollection<Availability> _contractorAvail = new ObservableCollection<Availability>();
        private ObservableCollection<ContractorSkills> _contractorSkills = new ObservableCollection<ContractorSkills>();
        private ObservableCollection<PreferredLocation> _contractorLocation = new ObservableCollection<PreferredLocation>();
        private ObservableCollection<Contractor> _contractors = new ObservableCollection<Contractor>();
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private ObservableCollection<Days> _days = new ObservableCollection<Days>();
        private Contractor _selectedContractor;
        private ContractorSkills _selectedSkill;
        private PreferredLocation _selectedLocation;
        private Days _selectedDays;
        //private int rowsAffected;
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
                OnPropertyChanged("SelectedContractor");
            }
        }

        public Days SelectedDays
        {
            get { return _selectedDays; }
            set { _selectedDays = value; }
        }

        public ObservableCollection<Days> Days
        {
            get { return _days; }
            set { _days = value; }
        }
       
        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
        }

        public AvailabilityVM()
        {

            var contractors = ContractorDB.GetAllContractors();
            foreach (var item in contractors)
            {
                Contractors.Add(item);
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

           
            //getAvailability(SelectedContractor);
            //getAvail(SelectedContractor);
        }

        public Availability[] getAvail(Contractor c)
        {

            DataTable availDays = new DataTable();
            var avail = AvailabilityDB.GetAllAvailability(c.contractorID);

            c.availabilities = new Availability[availDays.Rows.Count];
            int arrayCount = 0;
            foreach(DataRow dr in availDays.Rows)
            {                
                arrayCount++;
            }
            return c.availabilities;

            
        }

        //public void getAvailability(Contractor contractor)
        //{
        //    DataTable availDays = AvailabilityDB.GetAllAvailability();
        //    //ObservableCollection<Days> days = AvailabilityDB.GetAllDays();
        //    foreach (var drv in Days)
        //    {
        //        foreach (DataRow row in availDays.Rows)
        //        {
        //            if (drv.dayId.ToString() == row[0].ToString())
        //            {
        //                Days.Add(drv);
        //            }
        //        }
        //    }
        //}
    }
}
