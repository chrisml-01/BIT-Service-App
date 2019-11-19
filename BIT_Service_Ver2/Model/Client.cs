using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Model
{
    class Client : IValidatableObject
    {
        public int clientID { get; set; }
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == SurName)
            {
                yield return new ValidationResult("First name cannot match the last name");
            }

        }
    }
}
