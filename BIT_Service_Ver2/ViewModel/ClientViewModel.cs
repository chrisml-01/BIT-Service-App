using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
            var temp = ClientDB.GetAllClients(); 
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
            if(SelectedClient == null)
            {
                MessageBox.Show("Please select the last row before inserting.","Information",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else if (ValidateUser(SelectedClient) == 0)
            {

            }
            else if (ValidateUser(SelectedClient) == 1)
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
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void UpdateClient()
        {
            if(SelectedClient == null)
            {
                MessageBox.Show("Please make sure that you've selected a client to update.","Information",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else if (ValidateUser(SelectedClient) == 0)
            {
                
            }
            else if(ValidateUser(SelectedClient) == 1)
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
            else
            {
                MessageBox.Show("Error");
            }
           

        }

        private static int ValidateUser(Client client)
        {
            int result = 0;

            if (client.FirstName == client.SurName)
            {
                MessageBox.Show("First name and surname can't be similar. Please try again.");
                result = 0;
            }
            else if (client.DOB >= DateTime.Now)
            {
                MessageBox.Show("Date of birth should not be the date today or the future.");
                result = 0;
            }
            else if (client.MobileNum.Length > 11)
            {
                MessageBox.Show("Please make sure that your phone number is correct.");
            }
            else if(client.FirstName.Length > 180 && client.SurName.Length > 180 && client.Street.Length > 180 && client.Suburb.Length > 180 && client.Username.Length > 180 && client.Password.Length > 180)
            {
                MessageBox.Show("Please make sure that your input doesn't exceed 180 characters.");
                result = 0;
            }else if(client.Postcode.Length > 4)
            {
                MessageBox.Show("Please make sure that your postcode doesn't exceed 4 characters.");
                result = 0;
            }else
            {
                result = 1;
            }

            return result;
        }

    }
}
