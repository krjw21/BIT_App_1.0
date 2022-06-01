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
    public class Suburb : INotifyPropertyChanged
    {
        private string _suburbName;
        private string _postcode;
        private string _region;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public string SuburbName
        {
            get { return _suburbName; }
            set
            {
                _suburbName = value;
                OnPropertyChanged("SuburbName");
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
        public string Region
        {
            get { return _region; }
            set
            {
                _region = value;
                OnPropertyChanged("Region");
            }
        }


        public Suburb()
        {
            _db = new SQLHelper();
        }
        public Suburb(DataRow dr)
        {
            this.SuburbName = dr["Suburb_Name"].ToString();
            this.Postcode = dr["Postcode"].ToString();
            this.Region = dr["Region"].ToString();
            _db = new SQLHelper();
        }
    }
}
