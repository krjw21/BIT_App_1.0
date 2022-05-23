using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class ServiceRequest : INotifyPropertyChanged
    {
        private int _serviceRequestID;
        private int _clientID;
        private int? _contractorID;
        private int? _coordinatorID;
        private string _contractorName;
        private string _businessName;
        private string _contactName;
        private string _skillCategory;
        private string _priority;
        private string _jobStatus;
        private string _paymentStatus;
        private DateTime _dateCreated;
        private DateTime? _dateCompleted;
        private string _street;
        private string _suburb;
        private string _state;
        private string _postcode;
        private int? hoursWorked;
        private int? distanceTravelled;
        private SQLHelper _db;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public int ServiceRequestID 
        { 
            get => _serviceRequestID; 
            set => _serviceRequestID = value; 
        }
        public int ClientID 
        { 
            get => _clientID; 
            set => _clientID = value; 
        }
        public int? ContractorID 
        { 
            get => _contractorID; 
            set => _contractorID = value; 
        }
        public int? CoordinatorID 
        { 
            get => _coordinatorID; 
            set => _coordinatorID = value; 
        }
        public string ContractorName
        {
            get => _contractorName;
            set => _contractorName = value;
        }
        public string BusinessName
        {
            get { return _businessName; }
            set
            {
                _businessName = value;
                OnPropertyChanged("BusinessName");
            }
        }
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                OnPropertyChanged("ContactName");
            }
        }
        public string SkillCategory 
        { 
            get => _skillCategory;
            set 
            { 
                _skillCategory = value;
                OnPropertyChanged("SkillCategory");
            }
        }
        public string Priority 
        { 
            get => _priority;
            set 
            { 
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }
        public string JobStatus 
        { 
            get => _jobStatus;
            set 
            { 
                _jobStatus = value;
                OnPropertyChanged("JobStatus");
            }
        }
        public string PaymentStatus
        {
            get => _paymentStatus;
            set 
            { 
                _paymentStatus = value;
                OnPropertyChanged("PaymentStatus");
            }
        }
        public DateTime DateCreated 
        { 
            get => _dateCreated;
            set 
            { 
                _dateCreated = value;
                OnPropertyChanged("DateCreated");
            }
        }
        public DateTime? DateCompleted 
        { 
            get => _dateCompleted;
            set 
            { 
                _dateCompleted = value;
                OnPropertyChanged("DateCompleted");
            }
        }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }
        public string Suburb
        {
            get { return _suburb; }
            set
            {
                _suburb = value;
                OnPropertyChanged("Suburb");
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                OnPropertyChanged("Postcode");
            }
        }
        public int? HoursWorked 
        { 
            get => hoursWorked;
            set 
            { 
                hoursWorked = value;
                OnPropertyChanged("HoursWorked");
            }
        }
        public int? DistanceTravelled 
        { 
            get => distanceTravelled;
            set 
            { 
                distanceTravelled = value;
                OnPropertyChanged("DistanceTravelled");
            }
        }


        public ServiceRequest()
        {
            _db = new SQLHelper();
        }
        public ServiceRequest(DataRow dr)
        {
            _db = new SQLHelper();
            this.ServiceRequestID = Convert.ToInt32(dr["Service_Request_ID"].ToString());
            this.ClientID = Convert.ToInt32(dr["Client_ID"].ToString());
            bool success1 = int.TryParse(dr["Contractor_ID"].ToString(), out int contractorID);
            if (success1)
            {
                this.ContractorID = contractorID;
            }
            else
            {
                this.ContractorID = null;
            }
            bool success2 = int.TryParse(dr["Coordinator_ID"].ToString(), out int coordinatorID);
            if (success2)
            {
                this.CoordinatorID = coordinatorID;
            }
            else
            {
                this.CoordinatorID = null;
            }
            this.BusinessName = dr["Business_Name"].ToString();
            this.ContactName = $"{dr["First_Name"]} {dr["Last_Name"]}";
            this.SkillCategory = dr["Skill_Category"].ToString();
            this.Priority = dr["Priority"].ToString();
            this.JobStatus = dr["Job_Status"].ToString();
            this.PaymentStatus = dr["Payment_Status"].ToString();
            this.DateCreated = Convert.ToDateTime(dr["Date_Created"].ToString());
            bool success3 = DateTime.TryParse(dr["Date_Completed"].ToString(), out DateTime dateCompleted);
            if (success3)
            {
                this.DateCompleted = dateCompleted;
            }
            else
            {
                this.DateCompleted = null;
            }
            this.Street = dr["Street"].ToString();
            this.Suburb = dr["Suburb"].ToString();
            this.State = dr["State"].ToString();
            this.Postcode = dr["Postcode"].ToString();
            bool success4 = int.TryParse(dr["Hours_Worked"].ToString(), out int hoursWorked);
            if (success4)
            {
                this.HoursWorked = hoursWorked;
            }
            else
            {
                this.HoursWorked = null;
            }
            bool success5 = int.TryParse(dr["Distance_Travelled"].ToString(), out int distanceTravelled);
            if (success5)
            {
                this.DistanceTravelled = distanceTravelled;
            }
            else
            {
                this.DistanceTravelled = null;
            }
            //bool success6 = int.TryParse(dr["Contractor_First"].ToString(), out int contractorFirst);
            //bool success7 = int.TryParse(dr["Contractor_Last"].ToString(), out int contractorLast);
            //if (success6 && success7)
            //{
            //    this.ContractorName = $"{contractorFirst} {contractorLast}";
            //}
            //else
            //{
            //    this.ContractorName = null;
            //}
        }
    }
}
