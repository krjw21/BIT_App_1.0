using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Coordinator : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? _coordinatorID;
        private string _firstName;
        private string _lastName;
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


        public int? CoordinatorID
        {
            get { return _coordinatorID; }
            set { _coordinatorID = value; }
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


        public Coordinator()
        {
            _db = new SQLHelper();
        }
        public Coordinator(DataRow dr)
        {
            _db = new SQLHelper();
            this.CoordinatorID = Convert.ToInt32(dr["Coordinator_ID"].ToString());
            this.FirstName = dr["First_Name"].ToString();
            this.LastName = dr["Last_Name"].ToString();
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
        public string InsertCoordinator()
        {
            GeneratePassword();

            string sql = "INSERT INTO Coordinator(First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password, [Status]) VALUES(@FirstName, @LastName, @DOB, @Email, @Phone, @Street, @Suburb, @State, @Postcode, @Password, 1)";

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
                return $"New Co-ordinator: \"{FirstName} {LastName}\" was registered successfully.";
            }
            return "Registration was not successful, please try again.";
        }
        public string UpdateCoordinator()
        {
            string sql = "UPDATE Coordinator SET First_Name = @FirstName, Last_Name = @LastName, DOB = @DOB, Email = @Email, Phone = @Phone, Street = @Street, Suburb = @Suburb, [State] = @State, Postcode = @Postcode WHERE Coordinator_ID = @CoordinatorID";

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
            objParameters[9] = new SqlParameter("@CoordinatorID", DbType.String);
            objParameters[9].Value = this.CoordinatorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Details were updated successfully for the Co-ordinator: \"{FirstName} {LastName}\".";
            }
            return "Update of the Co-ordinator's details was not successful. Please try again.";
        }
        public string DeleteCoordinator()
        {
            string sql = "UPDATE Coordinator SET [Status] = 0 WHERE Coordinator_ID = @CoordinatorID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@CoordinatorID", DbType.String);
            objParameters[0].Value = this.CoordinatorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Record has been deactivated for the Co-ordinator: \"{FirstName} {LastName}\". Please contact an Administrator to revert or reinstate this record.";
            }
            return "Co-ordinator deactivation was unsuccessful. Please try again.";
        }
    }
}
