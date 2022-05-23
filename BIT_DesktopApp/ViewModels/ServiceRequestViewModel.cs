using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.ViewModels
{
    public class ServiceRequestViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ServiceRequest> _allServiceRequests;
        private ObservableCollection<ServiceRequest> _completedServiceRequests;
        private ObservableCollection<ServiceRequest> _unassignedServiceRequests;
        private ObservableCollection<JobState> _jobStates;
        private ObservableCollection<PaymentState> _paymentStates;
        private ObservableCollection<PriorityState> _priorityStates;
        private ObservableCollection<Skill> _skills;
        private ServiceRequest _selectedServiceRequest;
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
        public ObservableCollection <JobState> JobStates
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
        public ServiceRequest SelectedServiceRequest
        {
            get { return _selectedServiceRequest; }
            set 
            { 
                _selectedServiceRequest = value;
                OnPropertyChanged("SelectedServiceRequest");
            }
        }


        public ServiceRequestViewModel()
        {
            ServiceRequests allServiceRequests = new ServiceRequests();
            this.AllServiceRequests = new ObservableCollection<ServiceRequest>(allServiceRequests);

            ServiceRequests completedServiceRequests = new ServiceRequests("Completed");
            this.CompletedServiceRequests = new ObservableCollection<ServiceRequest>(completedServiceRequests);

            ServiceRequests unassignedServiceRequests = new ServiceRequests("Requested", "Rejected");
            this.UnassignedServiceRequests = new ObservableCollection<ServiceRequest>(unassignedServiceRequests);

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
