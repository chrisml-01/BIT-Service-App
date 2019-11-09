using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class Availability
    {
        public int ContractorID { get; set; }
        public int SlotId { get; set; }
        public string starT { get; set; }
        public string endT { get; set; }
        public int DayId { get; set; }
        public string dayName { get; set; }
        public int SkillId { get; set; }
        public string skillName { get; set; }
        public string Suburb { get; set; }


    }
}
