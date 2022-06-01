using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class AddContractorViewModel
    {
        private Contractor _newContractor;
        public Contractor NewContractor
        {
            get { return _newContractor; }
            set { _newContractor = value; }
        }


        // command for adding a new Contractor
        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.AddContractorMethod, true);
                }
                return _addCommand;
            }
            set { _addCommand = value; }
        }
        public void AddContractorMethod()
        {
            try
            {
                string message = NewContractor.InsertContractor();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show("FAILURE: New Co-ordinator registration was unsuccessful. Please try again or contact an Administrator.");
            }
        }


        public AddContractorViewModel()
        {
            NewContractor = new Contractor();
        }
    }
}
