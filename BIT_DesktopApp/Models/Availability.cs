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
    public class Availability : INotifyPropertyChanged
    {
        private int? _contractorID;
        private string _dayName;
        private string _startTime;
        private string _finishTime;
        private string _shiftType;
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
            get { return _contractorID; }
            set
            {
                _contractorID = value;
                OnPropertyChanged("ContractorID");
            }
        }
        public string DayName
        {
            get { return _dayName; }
            set
            {
                _dayName = value;
                OnPropertyChanged("Date");
            }
        }
        public string StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        public string FinishTime
        {
            get { return _finishTime; }
            set
            {
                _finishTime = value;
                OnPropertyChanged("FinishTime");
            }
        }
        public string ShiftType
        {
            get { return _shiftType; }
            set
            {
                _shiftType = value;
                OnPropertyChanged("ShiftType");
            }
        }


        public Availability()
        {
            _db = new SQLHelper();
        }
        public Availability(DataRow dr)
        {
            this.ContractorID = Convert.ToInt32(dr["Contractor_ID"].ToString());
            this.DayName = dr["Day_Name"].ToString();
            this.StartTime = dr["Start_Time"].ToString();
            this.FinishTime = dr["Finish_Time"].ToString();
            _db = new SQLHelper();
        }
        public Availability(int? id, string dayName, string shiftType)
        {
            this.ContractorID = id;
            this.DayName = dayName;
            this.ShiftType = shiftType;
            switch (shiftType)
            {
                case "Full Day Shift":
                    this.StartTime = "09:00";
                    this.FinishTime = "18:00";
                    break;
                case "Morning Shift":
                    this.StartTime = "09:00";
                    this.FinishTime = "13:30";
                    break;
                case "Afternoon Shift":
                    this.StartTime = "13:30";
                    this.FinishTime = "18:00";
                    break;
                case "Not Available":
                    this.StartTime = null;
                    this.FinishTime = null;
                    break;
            }
            _db = new SQLHelper();
        }

        public string UpdateAvailability()
        {
            string sql = "UPDATE Contractor_Availability SET Start_Time = @StartTime, Finish_Time = @FinishTime WHERE Day_Name = @DayName AND Contractor_ID = @ContractorID";
            SqlParameter[] objParameters = new SqlParameter[4];
            objParameters[0] = new SqlParameter("@StartTime", DbType.String);
            objParameters[0].Value = this.StartTime;
            objParameters[1] = new SqlParameter("@FinishTime", DbType.String);
            objParameters[1].Value = this.FinishTime;
            objParameters[2] = new SqlParameter("@DayName", DbType.String);
            objParameters[2].Value = this.DayName;
            objParameters[3] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[3].Value = this.ContractorID;

            string nullsql = "UPDATE Contractor_Availability SET Start_Time = NULL, Finish_Time = NULL WHERE Day_Name = @DayName AND Contractor_ID = @ContractorID";
            SqlParameter[] nullParameters = new SqlParameter[2];
            nullParameters[0] = new SqlParameter("@DayName", DbType.String);
            nullParameters[0].Value = this.DayName;
            nullParameters[1] = new SqlParameter("@ContractorID", DbType.Int32);
            nullParameters[1].Value = this.ContractorID;

            if (StartTime == null || FinishTime == null)
            {
                int rowsAffected = _db.ExecuteNonQuery(nullsql, nullParameters);
                if (rowsAffected >= 1)
                {
                    return $"Availability was updated for \"{DayName}\".";
                }
                return "Availability update was unsuccessful. Please try again.";
            }
            else
            {
                int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
                if (rowsAffected >= 1)
                {
                    return $"Availability was updated for \"{DayName}\".";
                }
                return "Availability update was unsuccessful. Please try again.";
            }
        }
    }
}
