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
    public class PriorityState : INotifyPropertyChanged
    {
        private string _priority;
        private SQLHelper _db;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }


        public PriorityState()
        {
            _db = new SQLHelper();
        }
        public PriorityState(DataRow dr)
        {
            this.Priority = dr["Priority"].ToString();
            _db = new SQLHelper();
        }
    }
}
