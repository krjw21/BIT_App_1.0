using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class AddClientViewModel
    {
        private Client _newClient;
        public Client NewClient
        {
            get { return _newClient; }
            set { _newClient = value; }
        }


        // command for adding a new Client
        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.AddClientMethod, true);
                }
                return _addCommand;
            }
            set { _addCommand = value; }
        }
        public void AddClientMethod()
        {
            try
            {
                string message = NewClient.InsertClient();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show("FAILURE: New Client registration was unsuccessful. Please try again or contact an Administrator.");
            }
        }


        public AddClientViewModel()
        {
            NewClient = new Client();
        }
    }
}
