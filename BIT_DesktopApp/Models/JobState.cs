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
    public class JobState : INotifyPropertyChanged
    {
        private string _jobStatus;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public string JobStatus
        {
            get { return _jobStatus; }
            set 
            { 
                _jobStatus = value;
                OnPropertyChanged("JobStatus");
            }
        }


        public JobState()
        {
            _db = new SQLHelper();
        }
        public JobState(DataRow dr)
        {
            this.JobStatus = dr["Job_Status"].ToString();
            _db = new SQLHelper();
        }
    }
}
