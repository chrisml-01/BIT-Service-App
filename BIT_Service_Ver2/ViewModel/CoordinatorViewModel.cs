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
    class CoordinatorViewModel : NotifyClass
    {
        private ObservableCollection<Coordinator> _coordinators = new ObservableCollection<Coordinator>();
        private Coordinator _selectedCoordinator;
        private int rowsAffected;
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
        public CoordinatorViewModel()
        {
            var temp = CoordinatorDB.GetAllCoordinators(); // This is a separate method so that we can move this in a DAL Layer
            foreach (var item in temp)
            {
                Coordinators.Add(item);
            }
        }

        public RelayCommand Add
        {
            get { return new RelayCommand(InsertCoord, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateCoord, true); }
        }


        private void InsertCoord()
        {
            if (ValidateUser(SelectedCoordinator) == 0)
            {

            }
            else if (ValidateUser(SelectedCoordinator) == 1)
            {
                rowsAffected = CoordinatorDB.insertCoordinator(SelectedCoordinator);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Coordinator added!");
                }
                else
                {
                    MessageBox.Show("insert failed!");
                }
            }
        }

        private void UpdateCoord()
        {
            if (ValidateUser(SelectedCoordinator) == 0)
            {

            }
            else if (ValidateUser(SelectedCoordinator) == 1) {
                
                    rowsAffected = CoordinatorDB.updateCoordinator(SelectedCoordinator);

                    if (rowsAffected != 0)
                    {
                        MessageBox.Show("Coordinator updated!");
                    }
                    else
                    {
                        MessageBox.Show("insert failed!");
                    }
                }
        }

        private static int ValidateUser(Coordinator coordinator)
        {
            int result = 0;

            if (coordinator.FirstName == coordinator.SurName)
            {
                MessageBox.Show("First name and surname can't be similar. Please try again.");
                result = 0;
            }
            else if (coordinator.DOB >= DateTime.Now)
            {
                MessageBox.Show("Date of birth should not be the date today or the future.");
                result = 0;
            }
            else if (coordinator.MobileNum.Length > 11)
            {
                MessageBox.Show("Please make sure that your phone number is correct.");
            }
            else if (coordinator.FirstName.Length > 180 && coordinator.SurName.Length > 180 && coordinator.Street.Length > 180 && coordinator.Suburb.Length > 180 && coordinator.Username.Length > 180 && coordinator.Password.Length > 180)
            {
                MessageBox.Show("Please make sure that your input doesn't exceed 180 characters.");
                result = 0;
            }
            else if (coordinator.Postcode.Length > 4)
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
