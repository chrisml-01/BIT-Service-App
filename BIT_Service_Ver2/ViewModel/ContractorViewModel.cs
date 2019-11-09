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
            var temp = ContractorDB.GetAllContractors(); // This is a separate method so that we can move this in a DAL Layer
            foreach (var item in temp)
            {
                Contractors.Add(item);
            }
        }

        public RelayCommand Add
        {
            get { return new RelayCommand(InsertContractor, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateContractor, true); }
        }


        private void InsertContractor()
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

        private void UpdateContractor()
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
}
