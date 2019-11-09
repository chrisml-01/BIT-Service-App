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

        private void UpdateCoord()
        {
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
}
