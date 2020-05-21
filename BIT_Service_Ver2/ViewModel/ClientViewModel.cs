using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Logger;
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
using System.Windows.Controls;

namespace BIT_Service_Ver2.ViewModel
{
    public class ClientViewModel : NotifyClass
    {
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private Client _selectedClient;
        private int rowsAffected;
        private string _input;
        //private string className = "ClientViewModel";
       

        //Will be binded for the SearchInput from the UC: Search
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }        
        
        //A list of all the clients and their details 
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        
        public Client SelectedClient
        {
            get { return _selectedClient;  }
            set { _selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        //Constructor
        public ClientViewModel()
        {
            var temp = ClientDB.GetAllClients(); 
            foreach (var item in temp)
            {
                Clients.Add(item);
            }
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertClient, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateClient, true); }
        }

        public RelayCommand Add
        {
            get { return new RelayCommand(AddClient, true); }
        }

        public RelayCommand Search
        {
            get { return new RelayCommand(SearchClient, true); }
        }


        //Method for adding another row to the data grid.
        private void AddClient()
        {
            int lastRow = Clients.Count;
            Client client = new Client();

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Clients.Add(client);
                }
            }
  
        }

        //Method for adding a client
        //Will be binded to the the 'Save' button command above
        private void InsertClient()
        {
            try
            {                
                if (SelectedClient == null)
                {
                    MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    //calling the method to validate the input from the model class (Client)
                    switch (SelectedClient.ValidateClient())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = ClientDB.insertClient(SelectedClient);
                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Client successfully added!");
                            }
                            break;
                    }
                }
            }
            catch(NullReferenceException e)
            {
                string result = "Insert failed! Please make sure that you've given all the necessary details to be added, please try again.";
                MessageBox.Show(result);
                //LogHelper.Log(LogTarget.Database, className, result);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //Method for updating client details
        //Will be binded to the 'Update' button command above
        private void UpdateClient()
        {
            try
            {
                if (SelectedClient == null)
                {
                    MessageBox.Show("Please make sure that you've selected a client to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    //calling the method to validate the input from the model class (Client)
                    switch (SelectedClient.ValidateClient())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = ClientDB.updateClient(SelectedClient);
                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Client succesfully updated!");
                            }
                            break;
                    }                   
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Update Failed! Please make sure that you've given the necessary details to be added, please try again.");                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //Method for searching a specific client information
        private void SearchClient()
        {
            Clients.Clear();
            var temp = ClientDB.SearchClient(Input);
            foreach (var item in temp)
            {
                Clients.Add(item);
            }

        }
    }
}
