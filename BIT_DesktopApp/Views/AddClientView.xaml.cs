using BIT_DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIT_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for AddClientView.xaml
    /// </summary>
    public partial class AddClientView : Window
    {
        public AddClientView()
        {
            InitializeComponent();
            this.DataContext = new AddClientViewModel();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtBusinessName.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtABN.Text = String.Empty;
            txtStreet.Text = String.Empty;
            txtSuburb.Text = String.Empty;
            txtState.Text = String.Empty;
            txtPostcode.Text = String.Empty;
        }

        
    }
}
