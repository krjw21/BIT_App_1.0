using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }


        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                EnableButtons = true;
                EnableFields = false;
                EnableUpdate = false;
            }
        }


        // command for editing a Client
        private bool _enableUpdate;
        private RelayCommand _updateClient;
        public bool EnableUpdate
        {
            get { return _enableUpdate; }
            set
            {
                _enableUpdate = value;
                OnPropertyChanged("EnableUpdate");

                if (SelectedClient != null)
                {
                    if (SelectedClient.ClientID != null)
                    {
                        if (value)
                        {
                            MessageBox.Show($"Updating details for the Client: \"{SelectedClient.BusinessName}\".");
                            EnableFields = true;
                            EnableAdd = false;
                        }
                        else
                        {
                            if (EnableFields == true)
                            {
                                UpdateClient.Execute(this);
                            }
                            EnableFields = false;
                            EnableAdd = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select a Client record before editing.");
                    }
                }
            }
        }
        public RelayCommand UpdateClient
        {
            get
            {
                if (_updateClient == null)
                {
                    _updateClient = new RelayCommand(this.UpdateClientMethod, true);
                }
                return _updateClient;
            }
            set { _updateClient = value; }
        }
        public void UpdateClientMethod()
        {
            try
            {
                string message = SelectedClient.UpdateClient();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with updating Client: \"{SelectedClient.BusinessName}\". Please try again or contact an Administrator.");
            }
            RefreshGrid();
            EnableButtons = false;
        }


        // command for deleting (deactivating) a Client
        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.DeleteClientMethod, true);
                }
                return _deleteCommand;
            }
            set { _deleteCommand = value; }
        }
        public void DeleteClientMethod()
        {

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Client: \"{SelectedClient.BusinessName}\"?", "Delete Confirmation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        string message = SelectedClient.DeleteClient();
                        MessageBox.Show(message);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"There was a problem with deleting this Client: \"{SelectedClient.BusinessName}\". Please try again or contact an Administrator.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            RefreshGrid();
        }


        private bool _enableFields;
        private bool _enableButtons;
        private bool _enableAdd;
        public bool EnableFields
        {
            get { return _enableFields; }
            set
            {
                _enableFields = value;
                OnPropertyChanged("EnableFields");
            }
        }
        public bool EnableButtons
        {
            get { return _enableButtons; }
            set
            {
                _enableButtons = value;
                OnPropertyChanged("EnableButtons");
            }
        }
        public bool EnableAdd
        {
            get { return _enableAdd; }
            set
            {
                _enableAdd = value;
                OnPropertyChanged("EnableAdd");
            }
        }


        // search filter functionality
        private string _searchText;
        private string _searchFilter;
        private RelayCommand _searchCommand;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public string SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                _searchFilter = value;
                OnPropertyChanged("SearchFilter");
            }
        }
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(this.SearchMethod, true);
                }
                return _searchCommand;
            }
            set { _searchCommand = value; }
        }
        public void SearchMethod()
        {
            if(SearchFilter != null)
            {
                Clients allClients = new Clients(SearchText, SearchFilter);
                this.Clients = new ObservableCollection<Client>(allClients);
            }
            else
            {
                MessageBox.Show("Please select a filter before searching for a record.");
            }

        }


        public void RefreshGrid()
        {
            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }


        public ClientViewModel()
        {
            EnableButtons = false;
            EnableAdd = true;
            RefreshGrid();
        }


        // Unit/Integration Test Method
        public virtual ObservableCollection<Client> GetClients()
        {
            Clients allClients = new Clients();
            return new ObservableCollection<Client>(allClients);
        }
    }
}
