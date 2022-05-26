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
    /// Interaction logic for AddServiceRequestView.xaml
    /// </summary>
    public partial class AddServiceRequestView : Window
    {
        public AddServiceRequestView()
        {
            InitializeComponent();
            this.DataContext = new AddServiceRequestViewModel();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cmbClientBusiness.SelectedIndex = -1;
            cmbSkillCategory.SelectedIndex = -1;
            cmbPriority.SelectedIndex = -1;
            dpDateCreated.SelectedDate = DateTime.MinValue;
            txtStreet.Text = String.Empty;
            txtSuburb.Text = String.Empty;
            txtState.Text = String.Empty;
            txtPostcode.Text = String.Empty;
        }
    }
}
