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
using System.Text.RegularExpressions;
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

        //A list of all the contractors and their details
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

        //Constructor
        public ContractorViewModel()
        {
            var temp = ContractorDB.GetAllContractors();
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }
        }

        //BUTTON COMMANDS
        //The following will be binded to button commands of UC (User Controls): Buttons
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


        //Method for adding a new row to the contractor data grid
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

        //Method for inserting a new contractor
        //Will be binded to the the 'Save' button command above
        private void InsertContractor()
        {
            try
            {
                if (SelectedContractor == null)
                {
                    MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (SelectedContractor.ValidateContractor())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = ContractorDB.insertContractor(SelectedContractor);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Contractor successfully added!");
                            }
                            break;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Insert failed! Please make sure that you've given the necessary details to be added, please try again.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Method for updating contractor details.
        //Will be binded to the 'Update' button command above
        private void UpdateContractor()
        {
            try
            {
                if (SelectedContractor == null)
                {
                    MessageBox.Show("Please make sure that you've selected a contractor to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (SelectedContractor.ValidateContractor())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = ContractorDB.updateContractor(SelectedContractor);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Contractor successfully updated!");
                            }
                            break;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Update failed! Please make sure that you've given the necessary details to be added, please try again.");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //Method for searching a specific contractor information
        private void SearchContractor()
        {
            Contractors.Clear();
            var temp = ContractorDB.SearchContractor(Input);
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }

        }
    }
}
