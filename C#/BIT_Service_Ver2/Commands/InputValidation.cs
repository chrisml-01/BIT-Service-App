using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_Service_Ver2.Commands
{
    public class InputValidation
    {
        private bool result = true;
        
        //CRUD Input Validation
        public bool Name(string firstname, string lastname)
        {
            if (firstname == "" || lastname == "")
            {
                result = false;
                MessageBox.Show("Please make sure that you've entered your full name");
            }
            else if (firstname.Length > 180 || lastname.Length > 180)
            {
                result = false;
                MessageBox.Show("Input must not exceed 180 characters");
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool DOB(DateTime dob)
        {
            if (dob == null)
            {
                result = false;
                MessageBox.Show("Date of birth can't be empty.");
            }
            else if (dob >= DateTime.Now)
            {
                result = false;
                MessageBox.Show("Date of birth should not be the date today or the future.");
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool Email(string email)
        {
            bool emailRegex = Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (email == "")
            {
                result = false;
                MessageBox.Show("Email should not be empty.");
            }
            else if (emailRegex == false)
            {
                result = false;
                MessageBox.Show("Invalid Email. Please try again.");
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool ContactNumber(string phone)
        {
            if (phone == "")
            {
                result = false;
                MessageBox.Show("Phone number can't be empty");
            }
            else if (phone.Length > 10)
            {
                result = false;
                MessageBox.Show("Please make sure that your phone number is correct.");

            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool Address(string street, string suburb, string state, string postcode)
        {
            if (street == "" || suburb == "" || state == "" || postcode == "")
            {
                result = false;
                MessageBox.Show("Please make sure that you've provided your full address.");
            }
            else if (street.Length > 180 || suburb.Length > 180 || state.Length > 180)
            {
                result = false;
                MessageBox.Show("Please make sure that your input doesn't exceed 180 characters");
            }
            else if (postcode.Length > 4)
            {
                result = false;
                MessageBox.Show("Please make sure that your postcode doesn't exceed 4 characters.");
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool LogOn(string username, string password)
        {
            if (username == "" || password == "")
            {
                result = false;
                MessageBox.Show("Username or Password can't be empty");
            }
            else if (username.Length <= 8 || password.Length <= 8)
            {
                result = false;
                MessageBox.Show("Please make sure that your username and password should atleast be 8 characters long.");
            }
            else
            {
                result = true;
            }

            return result;
        }

        //JOB REQUEST INPUT VALIDATION

        public bool BookingDate(DateTime bookingDate)
        {
            if (bookingDate == null)
            {
                result = false;
                MessageBox.Show("Booking date can't be empty.");
            }
            else if (bookingDate < DateTime.Now)
            {
                result = false;
                MessageBox.Show("Booking date shouldn't be a date from the past. Please try again.");
            }
            else
            {
                result = true;
            }


            return result;
        }

        public bool Notes(string notes)
        {
            if (notes.Length > 256)
            {
                result = false;
                MessageBox.Show("Please make sure that your input doesn't exceed 256 characters");
            }
            else
            {
                result = true;
            }

            return result;
        }

        //SKILLS INPUT VALIDATION
        public bool skillDescription(string desc)
        {
            if(desc.Length > 256)
            {
                result = false;
                MessageBox.Show("Please make sure that your description doesn't exceed 256 characters");
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool isChargeDouble(double charge)
        {
            if(charge.GetType() != typeof(double))
            {
                result = false;
                MessageBox.Show("Please make sure that your charge input is correct. Please try again.");
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
