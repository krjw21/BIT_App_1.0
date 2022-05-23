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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIT_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Page
    {
        public ClientView()
        {
            InitializeComponent();
            this.DataContext = new ClientViewModel();
        }

        private void btnSearchClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Checked(object sender, RoutedEventArgs e)
        {
            txtBusinessName.IsEnabled = true;
            txtABN.IsEnabled = true;
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtPhone.IsEnabled = true;
            txtStreet.IsEnabled = true;
            txtSuburb.IsEnabled = true;
            txtState.IsEnabled = true;
            txtPostcode.IsEnabled = true;
            btnUpdate.Content = "Save";
            btnDelete.IsEnabled = false;
            btnAdd.IsEnabled = false;
        }

        private void btnUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBusinessName.IsEnabled = false;
            txtABN.IsEnabled = false;
            txtFirstName.IsEnabled = false;
            txtLastName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtStreet.IsEnabled = false;
            txtSuburb.IsEnabled = false;
            txtState.IsEnabled = false;
            txtPostcode.IsEnabled = false;
            btnUpdate.Content = "Edit";
            btnDelete.IsEnabled = true;
            btnAdd.IsEnabled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddClientView();
            newWindow.ShowDialog();
        }
    }
}
