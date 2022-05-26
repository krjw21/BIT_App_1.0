using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class AddServiceRequestViewModel
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<PriorityState> _priorityStates;
        private ObservableCollection<Skill> _skills;
        private ServiceRequest _newServiceRequest;
        private RelayCommand _addCommand;


        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
            }
        }
        public ObservableCollection<PriorityState> PriorityStates
        {
            get { return _priorityStates; }
            set
            {
                _priorityStates = value;
            }
        }
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
            }
        }
        public ServiceRequest NewServiceRequest
        {
            get { return _newServiceRequest; }
            set { _newServiceRequest = value; }
        }
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.AddServiceRequestMethod, true);
                }
                return _addCommand;
            }
            set { _addCommand = value; }
        }


        public void AddServiceRequestMethod()
        {
            try
            {
                string message = NewServiceRequest.InsertServiceRequest();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show("FAILURE: New Service Request was unsuccessful. Please try again or contact an Administrator.");
            }
        }


        public AddServiceRequestViewModel()
        {
            NewServiceRequest = new ServiceRequest();

            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);

            PriorityStates priorityStates = new PriorityStates();
            this.PriorityStates = new ObservableCollection<PriorityState>(priorityStates);

            Skills skills = new Skills();
            this.Skills = new ObservableCollection<Skill>(skills);
        }
    }
}
