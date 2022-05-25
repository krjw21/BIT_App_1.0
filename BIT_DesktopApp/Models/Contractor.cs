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
    public class Contractor : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? _contractorID;
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private DateTime _dob;
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


        public int? ContractorID 
        { 
            get => _contractorID;
            set
            {
                _contractorID = value;
                OnPropertyChanged("ContractorID");
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
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public DateTime DOB
        {
            get { return _dob; }
            set
            {
                _dob = value;
                OnPropertyChanged("DOB");
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
                    case "DOB":
                        if (DOB > DateTime.Now || DOB.Year < 1900)
                        {
                            result = "Date of Birth must be a valid date.";
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


        public Contractor()
        {
            _db = new SQLHelper();
        }
        public Contractor(DataRow dr)
        {
            _db = new SQLHelper();
            this.ContractorID = Convert.ToInt32(dr["Contractor_ID"].ToString());
            this.FirstName = dr["First_Name"].ToString();
            this.LastName = dr["Last_Name"].ToString();
            this.FullName = dr["FullName"].ToString();
            this.DOB = Convert.ToDateTime(dr["DOB"].ToString());
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
        public string InsertContractor()
        {
            GeneratePassword();

            string sql = "INSERT INTO Contractor(First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password, [Status]) VALUES(@FirstName, @LastName, @DOB, @Email, @Phone, @Street, @Suburb, @State, @Postcode, @Password, 1)";

            SqlParameter[] objParameters = new SqlParameter[10];
            objParameters[0] = new SqlParameter("@FirstName", DbType.String);
            objParameters[0].Value = this.FirstName;
            objParameters[1] = new SqlParameter("@LastName", DbType.String);
            objParameters[1].Value = this.LastName;
            objParameters[2] = new SqlParameter("@DOB", DbType.DateTime);
            objParameters[2].Value = this.DOB;
            objParameters[3] = new SqlParameter("@Email", DbType.String);
            objParameters[3].Value = this.Email;
            objParameters[4] = new SqlParameter("@Phone", DbType.String);
            objParameters[4].Value = this.Phone;
            objParameters[5] = new SqlParameter("@Street", DbType.String);
            objParameters[5].Value = this.Street;
            objParameters[6] = new SqlParameter("@Suburb", DbType.String);
            objParameters[6].Value = this.Suburb;
            objParameters[7] = new SqlParameter("@State", DbType.String);
            objParameters[7].Value = this.State;
            objParameters[8] = new SqlParameter("@Postcode", DbType.String);
            objParameters[8].Value = this.Postcode;
            objParameters[9] = new SqlParameter("@Password", DbType.String);
            objParameters[9].Value = this.Password;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"New Contractor: \"{FirstName} {LastName}\" was registered successfully.";
            }
            return "Registration was not successful, please try again.";
        }
        public string UpdateContractor()
        {
            string sql = "UPDATE Contractor SET First_Name = @FirstName, Last_Name = @LastName, DOB = @DOB, Email = @Email, Phone = @Phone, Street = @Street, Suburb = @Suburb, [State] = @State, Postcode = @Postcode WHERE Contractor_ID = @ContractorID";

            SqlParameter[] objParameters = new SqlParameter[10];
            objParameters[0] = new SqlParameter("@FirstName", DbType.String);
            objParameters[0].Value = this.FirstName;
            objParameters[1] = new SqlParameter("@LastName", DbType.String);
            objParameters[1].Value = this.LastName;
            objParameters[2] = new SqlParameter("@DOB", DbType.DateTime);
            objParameters[2].Value = this.DOB;
            objParameters[3] = new SqlParameter("@Email", DbType.String);
            objParameters[3].Value = this.Email;
            objParameters[4] = new SqlParameter("@Phone", DbType.String);
            objParameters[4].Value = this.Phone;
            objParameters[5] = new SqlParameter("@Street", DbType.String);
            objParameters[5].Value = this.Street;
            objParameters[6] = new SqlParameter("@Suburb", DbType.String);
            objParameters[6].Value = this.Suburb;
            objParameters[7] = new SqlParameter("@State", DbType.String);
            objParameters[7].Value = this.State;
            objParameters[8] = new SqlParameter("@Postcode", DbType.String);
            objParameters[8].Value = this.Postcode;
            objParameters[9] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[9].Value = this.ContractorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Details were updated successfully for the Contractor: \"{FirstName} {LastName}\".";
            }
            return "Update of the Contractor's details was not successful. Please try again.";
        }
        public string DeleteContractor()
        {
            string sql = "UPDATE Contractor SET [Status] = 0 WHERE Contractor_ID = @ContractorID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[0].Value = this.ContractorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Record has been deactivated for the Contractor: \"{FirstName} {LastName}\". Please contact an Administrator to revert or reinstate this record.";
            }
            return "Contractor deactivation was unsuccessful. Please try again.";
        }
        public string AddSuburb(string suburb, string postcode)
        {
            string sql = "INSERT INTO Contractor_Suburb(Contractor_ID, Suburb_Name, Postcode) VALUES(@ContractorID, @Suburb, @Postcode)";
            SqlParameter[] objParameters = new SqlParameter[3];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[0].Value = this.ContractorID;
            objParameters[1] = new SqlParameter("@Suburb", DbType.String);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Postcode", DbType.String);
            objParameters[2].Value = postcode;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"New Suburb preference: \"{suburb}\" was added for the Contractor: \"{FirstName} {LastName}\".";
            }
            return "Suburb preference update was unsuccessful. Please try again.";
        }
        public string RemoveSuburb(string suburb, string postcode)
        {
            string sql = "DELETE FROM Contractor_Suburb WHERE Contractor_ID = @ContractorID AND Suburb_Name = @Suburb AND Postcode = @Postcode";
            SqlParameter[] objParameters = new SqlParameter[3];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[0].Value = this.ContractorID;
            objParameters[1] = new SqlParameter("@Suburb", DbType.String);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Postcode", DbType.String);
            objParameters[2].Value = postcode;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Suburb preference: \"{suburb}\" was removed for the Contractor: \"{FirstName} {LastName}\".";
            }
            return "Suburb preference removal was unsuccessful. Please try again.";
        }
    }
}
