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
    public class PaymentState : INotifyPropertyChanged
    {
        private string _paymentStatus;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public string PaymentStatus
        {
            get { return _paymentStatus; }
            set
            {
                _paymentStatus = value;
                OnPropertyChanged("PaymentStatus");
            }
        }


        public PaymentState()
        {
            _db = new SQLHelper();
        }
        public PaymentState(DataRow dr)
        {
            this.PaymentStatus = dr["Payment_Status"].ToString();
            _db = new SQLHelper();
        }
    }
}
