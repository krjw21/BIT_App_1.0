using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class ServiceRequest : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? _serviceRequestID;
        private int _clientID;
        private string _clientBusiness;
        private int? _contractorID;
        private int? _coordinatorID;
        private string _fullName;
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


        public int? ServiceRequestID 
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
            set
            {
                _contractorID = value;
                OnPropertyChanged("ContractorID");
            }
        }
        public int? CoordinatorID 
        { 
            get => _coordinatorID;
            set
            {
                _coordinatorID = value;
                OnPropertyChanged("CoordinatorID");
            }
        }
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
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


        public string Error { get { return null; } }
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "ClientID":
                        if (string.IsNullOrEmpty(ClientID.ToString()))
                        {
                            result = "Client ID field cannot be left empty.";
                        }
                        break;
                    case "BusinessName":
                        if (string.IsNullOrEmpty(BusinessName))
                        {
                            result = "Client's Business field cannot be left empty.";
                        }
                        break;
                    case "SkillCategory":
                        if (string.IsNullOrEmpty(SkillCategory))
                        {
                            result = "Skill Category field cannot be left empty.";
                        }
                        break;
                    case "Priority":
                        if (string.IsNullOrEmpty(Priority))
                        {
                            result = "Priority field cannot be left empty.";
                        }
                        break;
                    case "DateCreated":
                        if (DateCreated > DateTime.Now || DateCreated.Year < 1900)
                        {
                            result = "Date Requested field must be a valid date.";
                        }
                        break;
                    case "Street":
                        if (string.IsNullOrEmpty(Street))
                        {
                            result = "Street field cannot be left empty.";
                        }
                        break;
                    case "Suburb":
                        if (string.IsNullOrEmpty(Suburb))
                        {
                            result = "Suburb field cannot be left empty.";
                        }
                        break;
                    case "State":
                        if (string.IsNullOrEmpty(State))
                        {
                            result = "State field cannot be left empty.";
                        }
                        break;
                    case "Postcode":
                        if (string.IsNullOrEmpty(Postcode))
                        {
                            result = "Postcode field cannot be left empty.";
                        }
                        break;
                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection.Add(propertyName, result);
                    OnPropertyChanged("ErrorCollection");
                }
                return result;
            }
        }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();


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
            this.FullName = dr["FullName"].ToString();
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
        }

        
        public string InsertServiceRequest()
        {
            string sql = "INSERT INTO Service_Request(Client_ID, Skill_Category, [Priority], Job_Status, Payment_Status, Date_Created, Street, Suburb, [State], Postcode) VALUES((SELECT Client_ID FROM Client WHERE Business_Name = @BusinessName), @SkillCategory, @Priority, 'Requested', 'Unpaid', @DateCreated, @Street, @Suburb, @State, @Postcode)";
            SqlParameter[] objParameters = new SqlParameter[8];
            objParameters[0] = new SqlParameter("@BusinessName", DbType.String);
            objParameters[0].Value = this.BusinessName;
            objParameters[1] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[1].Value = this.SkillCategory;
            objParameters[2] = new SqlParameter("@Priority", DbType.String);
            objParameters[2].Value = this.Priority;
            objParameters[3] = new SqlParameter("@DateCreated", DbType.DateTime);
            objParameters[3].Value = this.DateCreated;
            objParameters[4] = new SqlParameter("@Street", DbType.String);
            objParameters[4].Value = this.Street;
            objParameters[5] = new SqlParameter("@Suburb", DbType.String);
            objParameters[5].Value = this.Suburb;
            objParameters[6] = new SqlParameter("@State", DbType.String);
            objParameters[6].Value = this.State;
            objParameters[7] = new SqlParameter("@Postcode", DbType.String);
            objParameters[7].Value = this.Postcode;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"New Service Request for the Client: \"{BusinessName}\" was requested successfully.";
            }
            return "New Service Request was not successful, please try again.";
        }
        public string UpdateServiceRequest()
        {
            string sql = "UPDATE Service_Request " +
                "SET Contractor_ID = (SELECT Contractor_ID FROM Contractor WHERE First_Name = @FirstName AND Last_Name = @LastName), " +
                "Skill_Category = @SkillCategory, " +
                "Priority = @Priority, " +
                "Job_Status = @JobStatus, " +
                "Payment_Status = @PaymentStatus, " +
                "Date_Created = @DateCreated, " +
                "Date_Completed = @DateCompleted, " +
                "Street = @Street, Suburb = @Suburb, " +
                "[State] = @State, Postcode = @Postcode, " +
                "Hours_Worked = @HoursWorked, " +
                "Distance_Travelled = @DistanceTravelled " +
                "WHERE Service_Request_ID = @ServiceRequestID";
            string[] contractorName = FullName.Split(' ');
            SqlParameter[] objParameters = new SqlParameter[15];
            objParameters[0] = new SqlParameter("@FirstName", DbType.String);
            objParameters[0].Value = contractorName[0];
            if (contractorName.Length < 2)
            {
                objParameters[1] = new SqlParameter("@LastName", DbType.String);
                objParameters[1].Value = ' ';
            }
            else
            {
                objParameters[1] = new SqlParameter("@LastName", DbType.String);
                objParameters[1].Value = contractorName[1];
            }
            objParameters[2] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[2].Value = this.SkillCategory;
            objParameters[3] = new SqlParameter("@Priority", DbType.String);
            objParameters[3].Value = this.Priority;
            objParameters[4] = new SqlParameter("@JobStatus", DbType.String);
            objParameters[4].Value = this.JobStatus;
            objParameters[5] = new SqlParameter("@PaymentStatus", DbType.String);
            objParameters[5].Value = this.PaymentStatus;
            objParameters[6] = new SqlParameter("@DateCreated", DbType.String);
            objParameters[6].Value = this.DateCreated;
            objParameters[7] = new SqlParameter("@DateCompleted", DbType.String);
            objParameters[7].Value = this.DateCompleted;
            objParameters[8] = new SqlParameter("@Street", DbType.String);
            objParameters[8].Value = this.Street;
            objParameters[9] = new SqlParameter("@Suburb", DbType.String);
            objParameters[9].Value = this.Suburb;
            objParameters[10] = new SqlParameter("@State", DbType.String);
            objParameters[10].Value = this.State;
            objParameters[11] = new SqlParameter("@Postcode", DbType.String);
            objParameters[11].Value = this.Postcode;
            objParameters[12] = new SqlParameter("@HoursWorked", DbType.String);
            objParameters[12].Value = this.HoursWorked;
            objParameters[13] = new SqlParameter("@DistanceTravelled", DbType.String);
            objParameters[13].Value = this.DistanceTravelled;
            objParameters[14] = new SqlParameter("@ServiceRequestID", DbType.String);
            objParameters[14].Value = this.ServiceRequestID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Details were updated successfully for Service Request ID: \"{ServiceRequestID}\".";
            }
            return "Update of the Service Request's details was not successful, please try again.";
        }
    }
}
