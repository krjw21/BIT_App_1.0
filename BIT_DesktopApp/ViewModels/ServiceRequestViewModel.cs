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
        private ObservableCollection<ServiceRequest> _allServiceRequests;
        private ObservableCollection<ServiceRequest> _completedServiceRequests;
        private ObservableCollection<ServiceRequest> _unassignedServiceRequests;
        private ObservableCollection<ServiceRequest> _assignedServiceRequests;
        private ObservableCollection<JobState> _jobStates;
        private ObservableCollection<PaymentState> _paymentStates;
        private ObservableCollection<PriorityState> _priorityStates;
        private ObservableCollection<Skill> _skills;
        private ObservableCollection<Contractor> _availableContractors;
        private ServiceRequest _selectedServiceRequest;
        private RelayCommand _updateServiceRequest;
        private bool _enableUpdate;
        private bool _enableFields;
        private bool _enableButtons;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public ObservableCollection<ServiceRequest> AllServiceRequests
        {
            get { return _allServiceRequests; }
            set
            {
                _allServiceRequests = value;
                OnPropertyChanged("AllServiceRequests");
            }
        }
        public ObservableCollection<ServiceRequest> CompletedServiceRequests
        {
            get { return _completedServiceRequests; }
            set
            {
                _completedServiceRequests = value;
                OnPropertyChanged("CompletedServiceRequests");
            }
        }
        public ObservableCollection<ServiceRequest> UnassignedServiceRequests
        {
            get { return _unassignedServiceRequests; }
            set
            {
                _unassignedServiceRequests = value;
                OnPropertyChanged("UnassignedServiceRequests");
            }
        }
        public ObservableCollection<ServiceRequest> AssignedServiceRequests
        {
            get { return _assignedServiceRequests; }
            set
            {
                _assignedServiceRequests = value;
                OnPropertyChanged("AssignedServiceRequests");
            }
        }
        public ObservableCollection<JobState> JobStates
        {
            get { return _jobStates; }
            set
            {
                _jobStates = value;
                OnPropertyChanged("JobStates");
            }
        }
        public ObservableCollection<PaymentState> PaymentStates
        {
            get { return _paymentStates; }
            set
            {
                _paymentStates = value;
                OnPropertyChanged("PaymentStates");
            }
        }
        public ObservableCollection<PriorityState> PriorityStates
        {
            get { return _priorityStates; }
            set
            {
                _priorityStates = value;
                OnPropertyChanged("PriorityStates");
            }
        }
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
            }
        }
        public ObservableCollection<Contractor> AvailableContractors
        {
            get { return _availableContractors; }
            set
            {
                _availableContractors = value;
                OnPropertyChanged("AvailableContractors");
            }
        }
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

                // TODO 1. No contractors available message display in datagrid AND 2. after updating, when switching tabs and clicking again, updated data dissapears
                if (SelectedServiceRequest != null)
                {
                    Contractors availableContractors = new Contractors(SelectedServiceRequest.SkillCategory, SelectedServiceRequest.Suburb, SelectedServiceRequest.DateCreated);
                    if (availableContractors.Count == 0 || availableContractors == null)
                    {
                        MessageBox.Show("There are no available Contractors for this job.");
                    }
                    this.AvailableContractors = new ObservableCollection<Contractor>(availableContractors);
                }
            }
        }
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
                        }
                        else
                        {
                            if (EnableFields == true)
                            {
                                UpdateServiceRequest.Execute(this);
                            }
                            EnableFields = false;
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
        public string UnassignedTabHeader { get; set; }
        public string CompletedTabHeader { get; set; }


        public void RefreshGrid()
        {
            ServiceRequests allServiceRequests = new ServiceRequests();
            this.AllServiceRequests = new ObservableCollection<ServiceRequest>(allServiceRequests);

            ServiceRequests completedServiceRequests = new ServiceRequests("Completed");
            this.CompletedServiceRequests = new ObservableCollection<ServiceRequest>(completedServiceRequests);
            int completedCount = CompletedServiceRequests.Count;
            CompletedTabHeader = $"Completed *{completedCount}";

            ServiceRequests unassignedServiceRequests = new ServiceRequests("Requested", "Rejected");
            this.UnassignedServiceRequests = new ObservableCollection<ServiceRequest>(unassignedServiceRequests);
            int unassignedCount = UnassignedServiceRequests.Count;
            UnassignedTabHeader = $"Requested/Rejected *{unassignedCount}";

            ServiceRequests assignedServiceRequests = new ServiceRequests("Assigned", "Accepted");
            this.AssignedServiceRequests = new ObservableCollection<ServiceRequest>(assignedServiceRequests);
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


        public ServiceRequestViewModel()
        {
            RefreshGrid();

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
