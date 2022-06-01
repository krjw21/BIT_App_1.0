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
    public class ServiceRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        private ObservableCollection<ServiceRequest> _allServiceRequests;
        public ObservableCollection<ServiceRequest> AllServiceRequests
        {
            get { return _allServiceRequests; }
            set
            {
                _allServiceRequests = value;
                OnPropertyChanged("AllServiceRequests");
            }
        }
        private ObservableCollection<ServiceRequest> _completedServiceRequests;
        public ObservableCollection<ServiceRequest> CompletedServiceRequests
        {
            get { return _completedServiceRequests; }
            set
            {
                _completedServiceRequests = value;
                OnPropertyChanged("CompletedServiceRequests");
            }
        }
        private ObservableCollection<ServiceRequest> _unassignedServiceRequests;
        public ObservableCollection<ServiceRequest> UnassignedServiceRequests
        {
            get { return _unassignedServiceRequests; }
            set
            {
                _unassignedServiceRequests = value;
                OnPropertyChanged("UnassignedServiceRequests");
            }
        }
        private ObservableCollection<ServiceRequest> _assignedServiceRequests;
        public ObservableCollection<ServiceRequest> AssignedServiceRequests
        {
            get { return _assignedServiceRequests; }
            set
            {
                _assignedServiceRequests = value;
                OnPropertyChanged("AssignedServiceRequests");
            }
        }
        private ObservableCollection<JobState> _jobStates;
        public ObservableCollection<JobState> JobStates
        {
            get { return _jobStates; }
            set
            {
                _jobStates = value;
                OnPropertyChanged("JobStates");
            }
        }
        private ObservableCollection<PaymentState> _paymentStates;
        public ObservableCollection<PaymentState> PaymentStates
        {
            get { return _paymentStates; }
            set
            {
                _paymentStates = value;
                OnPropertyChanged("PaymentStates");
            }
        }
        private ObservableCollection<PriorityState> _priorityStates;
        public ObservableCollection<PriorityState> PriorityStates
        {
            get { return _priorityStates; }
            set
            {
                _priorityStates = value;
                OnPropertyChanged("PriorityStates");
            }
        }
        private ObservableCollection<Skill> _skills;
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
            }
        }
        private ObservableCollection<Contractor> _availableContractors;
        public ObservableCollection<Contractor> AvailableContractors
        {
            get { return _availableContractors; }
            set
            {
                _availableContractors = value;
                OnPropertyChanged("AvailableContractors");
            }
        }


        private ServiceRequest _selectedServiceRequest;
        public ServiceRequest SelectedServiceRequest
        {
            get { return _selectedServiceRequest; }
            set
            {
                _selectedServiceRequest = value;
                OnPropertyChanged("SelectedServiceRequest");
                EnableButtons = true;
                EnableFields = false;
                EnableUpdate = false;

                if (SelectedServiceRequest != null)
                {
                    Contractors availableContractors = new Contractors(SelectedServiceRequest.ServiceRequestID, SelectedServiceRequest.SkillCategory, SelectedServiceRequest.Suburb, SelectedServiceRequest.DateCreated);
                    if (availableContractors.Count == 0 || availableContractors == null)
                    {
                        MessageBox.Show("There are no available Contractors for this job.");
                    }
                    this.AvailableContractors = new ObservableCollection<Contractor>(availableContractors);
                }
            }
        }


        // command for editing a Service Request
        private RelayCommand _updateServiceRequest;
        public RelayCommand UpdateServiceRequest
        {
            get
            {
                if (_updateServiceRequest == null)
                {
                    _updateServiceRequest = new RelayCommand(this.UpdateServiceRequestMethod, true);
                }
                return _updateServiceRequest;
            }
            set { _updateServiceRequest = value; }
        }
        public void UpdateServiceRequestMethod()
        {
            try
            {
                string message = SelectedServiceRequest.UpdateServiceRequest();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with updating Service Request ID: \"{SelectedServiceRequest.ServiceRequestID}\". Please try again or contact an Administrator.");
            }
            RefreshGrid();
            EnableButtons = false;
        }


        private bool _enableUpdate;
        private bool _enableFields;
        private bool _enableButtons;
        private bool _enableAdd;
        public bool EnableUpdate
        {
            get { return _enableUpdate; }
            set
            {
                _enableUpdate = value;
                OnPropertyChanged("EnableUpdate");

                if (SelectedServiceRequest != null)
                {
                    if (SelectedServiceRequest.ServiceRequestID != null)
                    {
                        if (value)
                        {
                            MessageBox.Show($"Updating details for Service Request ID: \"{SelectedServiceRequest.ServiceRequestID}\".");
                            EnableFields = true;
                            EnableAdd = false;
                        }
                        else
                        {
                            if (EnableFields == true)
                            {
                                UpdateServiceRequest.Execute(this);
                            }
                            EnableFields = false;
                            EnableAdd = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select a Service Request record before editing.");
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


        public string UnassignedTabHeader { get; set; }
        public string CompletedTabHeader { get; set; }


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
                ServiceRequests allServiceRequests = new ServiceRequests(SearchText, SearchFilter, false);
                this.AllServiceRequests = new ObservableCollection<ServiceRequest>(allServiceRequests);

                ServiceRequests completedServiceRequests = new ServiceRequests("Completed", SearchText, SearchFilter);
                this.CompletedServiceRequests = new ObservableCollection<ServiceRequest>(completedServiceRequests);

                ServiceRequests unassignedServiceRequests = new ServiceRequests("Requested", "Rejected", SearchText, SearchFilter);
                this.UnassignedServiceRequests = new ObservableCollection<ServiceRequest>(unassignedServiceRequests);

                ServiceRequests assignedServiceRequests = new ServiceRequests("Assigned", "Accepted", SearchText, SearchFilter);
                this.AssignedServiceRequests = new ObservableCollection<ServiceRequest>(assignedServiceRequests);
            }
            else
            {
                MessageBox.Show("Please select a filter before searching for a record.");
            }
        }


        public void RefreshGrid()
        {
            ServiceRequests allServiceRequests = new ServiceRequests();
            this.AllServiceRequests = new ObservableCollection<ServiceRequest>(allServiceRequests);

            ServiceRequests completedServiceRequests = new ServiceRequests("Completed");
            this.CompletedServiceRequests = new ObservableCollection<ServiceRequest>(completedServiceRequests);
            int completedCount = CompletedServiceRequests.Count;
            CompletedTabHeader = $"Completed *{completedCount}";

            ServiceRequests unassignedServiceRequests = new ServiceRequests("Requested", "Rejected", true);
            this.UnassignedServiceRequests = new ObservableCollection<ServiceRequest>(unassignedServiceRequests);
            int unassignedCount = UnassignedServiceRequests.Count;
            UnassignedTabHeader = $"Requested/Rejected *{unassignedCount}";

            ServiceRequests assignedServiceRequests = new ServiceRequests("Assigned", "Accepted", true);
            this.AssignedServiceRequests = new ObservableCollection<ServiceRequest>(assignedServiceRequests);
        }
        

        public ServiceRequestViewModel()
        {
            RefreshGrid();
            EnableAdd = true;

            JobStates jobStates = new JobStates();
            this.JobStates = new ObservableCollection<JobState>(jobStates);

            PaymentStates paymentStates = new PaymentStates();
            this.PaymentStates = new ObservableCollection<PaymentState>(paymentStates);

            PriorityStates priorityStates = new PriorityStates();
            this.PriorityStates = new ObservableCollection<PriorityState>(priorityStates);

            Skills skills = new Skills();
            this.Skills = new ObservableCollection<Skill>(skills);
        }
    }
}
