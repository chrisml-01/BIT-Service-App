using BIT_Service_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class Skill: NotifyClass
    {
        public int skillID { get; set; }
        public string skillName { get; set; }
        public string description { get; set; }
        public double charge { get; set; }
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
