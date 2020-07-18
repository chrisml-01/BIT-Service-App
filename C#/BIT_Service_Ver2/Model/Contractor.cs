using BIT_Service_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class Contractor
    {
        private InputValidation val = new InputValidation();

        public int contractorID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DOB { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string MobileNum { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ObservableCollection<Days> availabilities;
        public ObservableCollection<Skill> skills;

        public int ValidateContractor()
        {
            int result = 1;

            if (val.Name(FirstName, SurName) == false || val.DOB(DOB) == false || val.Email(Email) == false || val.ContactNumber(MobileNum) == false
                || val.Address(Street, Suburb, State, Postcode) == false || val.LogOn(Username, Password) == false)
            {
                result = 0;
            }

            return result;
        }
    }
}
