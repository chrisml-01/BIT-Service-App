using BIT_Service_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class Availability: NotifyClass
    {
        public int ContractorID { get; set; }
        public int SlotId { get; set; }
        public string starT { get; set; }
        public string endT { get; set; }
        public int DayId { get; set; }
        public string dayName { get; set; }
        private bool _isChecked = false;
        public bool isChecked
        {
            get => _isChecked;
            set
            {
                if(value != this._isChecked)
                {
                    this._isChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }
 
    }

    class ContractorSkills
    {
        public int ContractorID { get; set; }
        public int SkillId { get; set; }
        public string skillName { get; set; }
    }

    class PreferredLocation
    {
        public int ContractorID { get; set; }
        public string Suburb { get; set; }
    }

    class Days: NotifyClass
    {
        public int dayId { get; set; }
        public string dayName { get; set; }
        private bool _isChecked = false;
        public bool isChecked
        {
            get => _isChecked;
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }
    }

    class TimeSlot: NotifyClass
    {
        public int slotId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        private bool _isChecked = false;
        public bool isChecked
        {
            get => _isChecked;
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }

    }
}
