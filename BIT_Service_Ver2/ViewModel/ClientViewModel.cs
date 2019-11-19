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
    class ClientViewModel : NotifyClass
    {
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client _selectedClient;
        private Client _newClient;
        private int rowsAffected;
        
       
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        //do a selectedClient method here (Interface Lesson) 
        public Client SelectedClient
        {
            get { return _selectedClient;  }
            set { _selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public Client NewClient
        {
            get { return _newClient; }
            set { _newClient = value;}
        }

        public ClientViewModel()
        {
            var temp = ClientDB.GetAllClients(); // This is a separate method so that we can move this in a DAL Layer
            foreach (var item in temp)
            {
                Clients.Add(item);
            }
        }

        public RelayCommand Add
        {
            get { return new RelayCommand(InsertClient, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateClient, true); }
        }


        private void InsertClient()
        {

            rowsAffected = ClientDB.insertClient(SelectedClient);

            

            if (rowsAffected != 0)
            {
                MessageBox.Show("client added!");
            }
            else
            {
                MessageBox.Show("insert failed!");
            }

        }

        private void UpdateClient()
        {
            rowsAffected = ClientDB.updateClient(SelectedClient);

            if (rowsAffected != 0)
            {
                MessageBox.Show("client updated!");
            }
            else
            {
                MessageBox.Show("update failed!");
            }

        }

    }
}
