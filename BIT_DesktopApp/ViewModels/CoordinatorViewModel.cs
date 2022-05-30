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
    public class CoordinatorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Coordinator> _coordinators;
        private Coordinator _selectedCoordinator;
        private RelayCommand _updateCoordinator;
        private RelayCommand _deleteCommand;
        private bool _enableUpdate;
        private bool _enableFields;
        private bool _enableButtons;
        private bool _enableAdd;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public ObservableCollection<Coordinator> Coordinators
        {
            get { return _coordinators; }
            set
            {
                _coordinators = value;
                OnPropertyChanged("Coordinators");
            }
        }
        public Coordinator SelectedCoordinator
        {
            get { return _selectedCoordinator; }
            set
            {
                _selectedCoordinator = value;
                OnPropertyChanged("SelectedCoordinator");
                EnableButtons = true;
                EnableFields = false;
                EnableUpdate = false;
            }
        }
        public RelayCommand UpdateCoordinator
        {
            get
            {
                if (_updateCoordinator == null)
                {
                    _updateCoordinator = new RelayCommand(this.UpdateCoordinatorMethod, true);
                }
                return _updateCoordinator;
            }
            set { _updateCoordinator = value; }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.DeleteCoordinatorMethod, true);
                }
                return _deleteCommand;
            }
            set { _deleteCommand = value; }
        }
        public bool EnableUpdate
        {
            get { return _enableUpdate; }
            set
            {
                _enableUpdate = value;
                OnPropertyChanged("EnableUpdate");

                if (SelectedCoordinator != null)
                {
                    if (SelectedCoordinator.CoordinatorID != null)
                    {
                        if (value)
                        {
                            MessageBox.Show($"Updating details for the Co-ordinator: \"{SelectedCoordinator.FirstName} {SelectedCoordinator.LastName}\".");
                            EnableFields = true;
                            EnableAdd = false;
                        }
                        else
                        {
                            if (EnableFields == true)
                            {
                                UpdateCoordinator.Execute(this);
                            }
                            EnableFields = false;
                            EnableAdd = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select a Co-ordinator record before editing.");
                    }
                }
            }
        }
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
            if (SearchFilter != null)
            {
                Coordinators allCoordinators = new Coordinators(SearchText, SearchFilter);
                this.Coordinators = new ObservableCollection<Coordinator>(allCoordinators);
            }
            else
            {
                MessageBox.Show("Please select a filter before searching for a record.");
            }
        }

        public void RefreshGrid()
        {
            Coordinators allCoordinators = new Coordinators();
            this.Coordinators = new ObservableCollection<Coordinator>(allCoordinators);
        }
        public void UpdateCoordinatorMethod()
        {
            try
            {
                string message = SelectedCoordinator.UpdateCoordinator();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with updating Co-ordinator: \"{SelectedCoordinator.FirstName} {SelectedCoordinator.LastName}\". Please try again or contact an Administrator.");
            }
            RefreshGrid();
            EnableButtons = false;
        }
        public void DeleteCoordinatorMethod()
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Co-ordinator: \"{SelectedCoordinator.FirstName} {SelectedCoordinator.LastName}\"?", "Delete Confirmation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        string message = SelectedCoordinator.DeleteCoordinator();
                        MessageBox.Show(message);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"There was a problem with deleting this Co-ordinator: \"{SelectedCoordinator.FirstName} {SelectedCoordinator.LastName}\". Please try again or contact an Administrator.");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            RefreshGrid();
        }


        public CoordinatorViewModel()
        {
            EnableButtons = false;
            EnableAdd = true;
            RefreshGrid();
        }
    }
}
