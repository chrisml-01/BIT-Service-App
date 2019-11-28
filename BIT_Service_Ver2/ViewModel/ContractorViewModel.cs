using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_Service_Ver2.ViewModel
{
    class ContractorViewModel : NotifyClass
    {
        private ObservableCollection<Contractor> _contractors = new ObservableCollection<Contractor>();
        private Contractor _selectedContractor;
        private int rowsAffected;
        private string _input;
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }
        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set { _contractors = value; }
        }
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set { _selectedContractor = value;
                OnPropertyChanged("SelectedContractor");
            }
        }
        public ContractorViewModel()
        {
            var temp = ContractorDB.GetAllContractors(); 
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }
        }

        public RelayCommand Save
        {
            get { return new RelayCommand(InsertContractor, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateContractor, true); }
        }

        public RelayCommand Search
        {
            get { return new RelayCommand(SearchContractor, true); }
        }
        public RelayCommand Add
        {
            get { return new RelayCommand(AddContractor, true); }
        }

        private void AddContractor()
        {
            int lastRow = Contractors.Count;
            Contractor contractor = new Contractor();

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Contractors.Add(contractor);
                }
            }

        }

        private void InsertContractor()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(ValidateUser(SelectedContractor) == 0)
            {

            }
            else if (ValidateUser(SelectedContractor) == 1)
            {
                rowsAffected = ContractorDB.insertContractor(SelectedContractor);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("contractor added!");
                }
                else
                {
                    MessageBox.Show("insert failed!");
                }
            }
        }

        private void UpdateContractor()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please make sure that you've selected a contractor to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(ValidateUser(SelectedContractor) == 0)
            {

            }
            else if (ValidateUser(SelectedContractor) == 1)
            {
                rowsAffected = ContractorDB.updateContractor(SelectedContractor);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("contractor updated!");
                }
                else
                {
                    MessageBox.Show("insert failed!");
                }
            }
        }

        private void SearchContractor()
        {
            Contractors.Clear();
            var temp = ContractorDB.SearchContractor(Input);
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }

        }

        private static int ValidateUser(Contractor contractor)
        {
            int result = 0;

            if (contractor.FirstName == contractor.SurName)
            {
                MessageBox.Show("First name and surname can't be similar. Please try again.");
                result = 0;
            }
            else if (contractor.DOB >= DateTime.Now)
            {
                MessageBox.Show("Date of birth should not be the date today or the future.");
                result = 0;
            }
            else if (contractor.MobileNum.Length > 11)
            {
                MessageBox.Show("Please make sure that your phone number is correct.");
            }
            else if (contractor.FirstName.Length > 180 && contractor.SurName.Length > 180 && contractor.Street.Length > 180 && contractor.Suburb.Length > 180 && contractor.Username.Length > 180 && contractor.Password.Length > 180)
            {
                MessageBox.Show("Please make sure that your input doesn't exceed 180 characters.");
                result = 0;
            }
            else if (contractor.Postcode.Length > 4)
            {
                MessageBox.Show("Please make sure that your postcode doesn't exceed 4 characters.");
                result = 0;
            }
            else
            {
                result = 1;
            }

            return result;
        }

    }
}
