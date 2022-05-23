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
    /// Interaction logic for ServiceRequestView.xaml
    /// </summary>
    public partial class ServiceRequestView : Page
    {
        public ServiceRequestView()
        {
            InitializeComponent();
            this.DataContext = new ServiceRequestViewModel();
        }

        private void btnSearchServiceRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Checked(object sender, RoutedEventArgs e)
        {
            txtBusinessName.IsEnabled = true;
            txtContactName.IsEnabled = true;
            dpDateCreated.IsEnabled = true;
            dpDateCompleted.IsEnabled = true;
            cmbJobStatus.IsEnabled = true;
            cmbPaymentStatus.IsEnabled = true;
            txtAssignedContractor.IsEnabled = true;
            txtStreet.IsEnabled = true;
            txtSuburb.IsEnabled = true;
            txtState.IsEnabled = true;
            txtPostcode.IsEnabled = true;
            cmbPriority.IsEnabled = true;
            cmbSkillCategory.IsEnabled = true;
            txtHoursWorked.IsEnabled = true;
            txtDistanceTravelled.IsEnabled = true;
            btnUpdate.Content = "Save";
        }

        private void btnUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBusinessName.IsEnabled = false;
            txtContactName.IsEnabled = false;
            dpDateCreated.IsEnabled = false;
            dpDateCompleted.IsEnabled = false;
            cmbJobStatus.IsEnabled = false;
            cmbPaymentStatus.IsEnabled = false;
            txtAssignedContractor.IsEnabled = false;
            txtStreet.IsEnabled = false;
            txtSuburb.IsEnabled = false;
            txtState.IsEnabled = false;
            txtPostcode.IsEnabled = false;
            cmbPriority.IsEnabled = false;
            cmbSkillCategory.IsEnabled = false;
            txtHoursWorked.IsEnabled = false;
            txtDistanceTravelled.IsEnabled = false;
            btnUpdate.Content = "Edit";
        }
    }
}
