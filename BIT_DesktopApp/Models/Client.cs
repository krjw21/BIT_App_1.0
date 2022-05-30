using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BIT_DesktopApp.Logs.LogHelper;

namespace BIT_DesktopApp.Models
{
    public class Client : INotifyPropertyChanged, IDataErrorInfo
    {
        public static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private int? _clientID;
        private string _businessName;
        private string _abn;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _street;
        private string _suburb;
        private string _state;
        private string _postcode;
        private string _password;
        private SQLHelper _db;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public int? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
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
        public string ABN
        {
            get { return _abn; }
            set
            {
                _abn = value;
                OnPropertyChanged("ABN");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
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
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
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
                    case "BusinessName":
                        if (string.IsNullOrEmpty(BusinessName))
                        {
                            result = "Business Name field cannot be left empty.";
                        }
                        break;
                    case "ABN":
                        if (string.IsNullOrEmpty(ABN))
                        {
                            result = "ABN field cannot be left empty.";
                        }
                        break;
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "First Name field cannot be left empty.";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                        {
                            result = "Last Name field cannot be left empty.";
                        }
                        break;
                    case "Email":
                        if (string.IsNullOrEmpty(Email))
                        {
                            result = "Email field cannot be left empty.";
                        }
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(Phone))
                        {
                            result = "Phone field cannot be left empty.";
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


        public Client()
        {
            _db = new SQLHelper();
        }
        public Client(bool createHelper = true)
        {
            if (createHelper)
            {
                _db = new SQLHelper();
            }
        }
        public Client(DataRow dr)
        {
            _db = new SQLHelper();
            this.ClientID = Convert.ToInt32(dr["Client_ID"].ToString());
            this.BusinessName = dr["Business_Name"].ToString();
            this.ABN = dr["ABN"].ToString();
            this.FirstName = dr["First_Name"].ToString();
            this.LastName = dr["Last_Name"].ToString();
            this.Email = dr["Email"].ToString();
            this.Phone = dr["Phone"].ToString();
            this.Street = dr["Street"].ToString();
            this.Suburb = dr["Suburb"].ToString();
            this.State = dr["State"].ToString();
            this.Postcode = dr["Postcode"].ToString();
        }


        private void GeneratePassword()
        {
            this.Password = "test";
        }
        public string InsertClient()
        {
            GeneratePassword();

            string sql = "INSERT INTO Client(Business_Name, ABN, First_Name, Last_Name, Email, Phone, Street, Suburb, [State], Postcode, Password, [Status]) VALUES(@BusinessName, @ABN, @FirstName, @LastName, @Email, @Phone, @Street, @Suburb, @State, @Postcode, @Password, 1)";

            SqlParameter[] objParameters = new SqlParameter[11];
            objParameters[0] = new SqlParameter("@BusinessName", DbType.String);
            objParameters[0].Value = this.BusinessName;
            objParameters[1] = new SqlParameter("@ABN", DbType.String);
            objParameters[1].Value = this.ABN;
            objParameters[2] = new SqlParameter("@FirstName", DbType.String);
            objParameters[2].Value = this.FirstName;
            objParameters[3] = new SqlParameter("@LastName", DbType.String);
            objParameters[3].Value = this.LastName;
            objParameters[4] = new SqlParameter("@Email", DbType.String);
            objParameters[4].Value = this.Email;
            objParameters[5] = new SqlParameter("@Phone", DbType.String);
            objParameters[5].Value = this.Phone;
            objParameters[6] = new SqlParameter("@Street", DbType.String);
            objParameters[6].Value = this.Street;
            objParameters[7] = new SqlParameter("@Suburb", DbType.String);
            objParameters[7].Value = this.Suburb;
            objParameters[8] = new SqlParameter("@State", DbType.String);
            objParameters[8].Value = this.State;
            objParameters[9] = new SqlParameter("@Postcode", DbType.String);
            objParameters[9].Value = this.Postcode;
            objParameters[10] = new SqlParameter("@Password", DbType.String);
            objParameters[10].Value = this.Password;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                Log(LogTarget.File, $"SUCCESS: New Client: {BusinessName} was inserted successfully.");
                logger.Info($"SUCCESS: New Client: {BusinessName} was inserted successfully.");

                return $"New Client: \"{BusinessName}\" was registered successfully.";
            }
            Log(LogTarget.File, $"FAILURE: New Client: {BusinessName} insertion was unsuccessful.");
            logger.Info($"FAILURE: New Client: {BusinessName} insertion was unsuccessful.");

            return "Registration was not successful, please try again.";
        }
        public string UpdateClient()
        {
            string sql = "UPDATE Client SET Business_Name = @BusinessName, ABN = @ABN, First_Name = @FirstName, Last_Name = @LastName, Email = @Email, Phone = @Phone, Street = @Street, Suburb = @Suburb, [State] = @State, Postcode = @Postcode WHERE Client_ID = @ClientID";

            SqlParameter[] objParameters = new SqlParameter[11];
            objParameters[0] = new SqlParameter("@BusinessName", DbType.String);
            objParameters[0].Value = this.BusinessName;
            objParameters[1] = new SqlParameter("@ABN", DbType.String);
            objParameters[1].Value = this.ABN;
            objParameters[2] = new SqlParameter("@FirstName", DbType.String);
            objParameters[2].Value = this.FirstName;
            objParameters[3] = new SqlParameter("@LastName", DbType.String);
            objParameters[3].Value = this.LastName;
            objParameters[4] = new SqlParameter("@Email", DbType.String);
            objParameters[4].Value = this.Email;
            objParameters[5] = new SqlParameter("@Phone", DbType.String);
            objParameters[5].Value = this.Phone;
            objParameters[6] = new SqlParameter("@Street", DbType.String);
            objParameters[6].Value = this.Street;
            objParameters[7] = new SqlParameter("@Suburb", DbType.String);
            objParameters[7].Value = this.Suburb;
            objParameters[8] = new SqlParameter("@State", DbType.String);
            objParameters[8].Value = this.State;
            objParameters[9] = new SqlParameter("@Postcode", DbType.String);
            objParameters[9].Value = this.Postcode;
            objParameters[10] = new SqlParameter("@ClientID", DbType.String);
            objParameters[10].Value = this.ClientID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                Log(LogTarget.File, $"SUCCESS: Updated details for Client: {BusinessName}.");
                logger.Info($"SUCCESS: Updated details for Client: {BusinessName}.");

                return $"Details were updated successfully for the Client: \"{BusinessName}\".";
            }
            Log(LogTarget.File, $"FAILURE: Update details for Client: {BusinessName} unsuccessful.");
            logger.Debug($"FAILURE: Update details for Client: {BusinessName} unsuccessful.");

            return "Update of the Client's details was not successful, please try again.";
        }
        public string DeleteClient()
        {
            string sql = "UPDATE Client SET [Status] = 0 WHERE Client_ID = @ClientID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ClientID", DbType.String);
            objParameters[0].Value = this.ClientID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                Log(LogTarget.File, $"SUCCESS: Client: {BusinessName} record deactivated.");
                logger.Info($"SUCCESS: Client: {BusinessName} record deactivated.");

                return $"Record has been deactivated for the Client: \"{BusinessName}\". Please contact an Administrator to revert or reinstate this record.";
            }
            Log(LogTarget.File, $"FAILURE: Record deactivation for Client: {BusinessName} unsuccessful.");
            logger.Debug($"FAILURE: Record deactivation for Client: {BusinessName} unsuccessful.");

            return "Co-ordinator deactivation was not successful, please try again.";
        }
    }
}
