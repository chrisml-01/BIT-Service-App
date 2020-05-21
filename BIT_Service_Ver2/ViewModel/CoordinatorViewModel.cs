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
    class CoordinatorViewModel : NotifyClass
    {
        private ObservableCollection<Coordinator> _coordinators = new ObservableCollection<Coordinator>();
        private Coordinator _selectedCoordinator;
        private int rowsAffected;
        private string _input;
       
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        //A list of all the coordinators and their details
        public ObservableCollection<Coordinator> Coordinators
        {
            get { return _coordinators; }
            set { _coordinators = value; }
        }
        public Coordinator SelectedCoordinator
        {
            get { return _selectedCoordinator; }
            set
            {
                _selectedCoordinator = value;
                OnPropertyChanged("SelectedCoordinator");
            }
        }

        //constructor
        public CoordinatorViewModel()
        {
            var temp = CoordinatorDB.GetAllCoordinators(); 
            foreach (var item in temp)
            {
                Coordinators.Add(item);
            }
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertCoord, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateCoord, true); }
        }
        public RelayCommand Search
        {
            get { return new RelayCommand(SearchCoordinator, true); }
        }
        public RelayCommand Add
        {
            get { return new RelayCommand(AddCoordinator, true); }
        }

        //Method for adding a new row to the contractor data grid
        private void AddCoordinator()
        {
            int lastRow = Coordinators.Count;
            Coordinator coordinator = new Coordinator();

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Coordinators.Add(coordinator);
                }
            }

        }

        //Method for inserting a new coordinator
        //Will be binded to the the 'Save' button command above
        private void InsertCoord()
        {
            try
            {
                if (SelectedCoordinator == null)
                {
                    MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (SelectedCoordinator.ValidateCoordinator())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = CoordinatorDB.insertCoordinator(SelectedCoordinator);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Coordinator successfully added!");
                            }
                            break;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Insert Failed! Please make sure that you've given all the necessary details to be added, please try again.");
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        //Method for updating coordinator details.
        //Will be binded to the 'Update' button command above
        private void UpdateCoord()
        {
            try
            {
                if (SelectedCoordinator == null)
                {
                    MessageBox.Show("Please make sure that you've selected a coordinator to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (SelectedCoordinator.ValidateCoordinator())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = CoordinatorDB.updateCoordinator(SelectedCoordinator);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Coordinator successfully updated!");
                            }
                            else
                            {
                                MessageBox.Show("Update failed! Please try again.");
                            }
                            break;
                    }
                }
            }catch (Exception e)
            {
                throw e;
            }
           
        }

        //Method for searching a specific coordinator information
        private void SearchCoordinator()
        {
            Coordinators.Clear();
            var temp = CoordinatorDB.SearchCoordinator(Input);
            foreach (var item in temp)
            {
                Coordinators.Add(item);
            }
        }
  
    }
}
