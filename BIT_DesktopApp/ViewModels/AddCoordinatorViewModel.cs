using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class AddCoordinatorViewModel
    {
        private Coordinator _newCoordinator;
        public Coordinator NewCoordinator
        {
            get { return _newCoordinator; }
            set { _newCoordinator = value; }
        }


        // command for adding a new Coordinator
        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.AddCoordinatorMethod, true);
                }
                return _addCommand;
            }
            set { _addCommand = value; }
        }
        public void AddCoordinatorMethod()
        {
            try
            {
                string message = NewCoordinator.InsertCoordinator();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show("FAILURE: New Co-ordinator registration was unsuccessful. Please try again or contact an Administrator.");
            }
        }


        public AddCoordinatorViewModel()
        {
            NewCoordinator = new Coordinator();
        }
    }
}
