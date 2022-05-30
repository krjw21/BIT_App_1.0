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
using BIT_DesktopApp.Models;
using BIT_DesktopApp.Views;

namespace BIT_DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // STYLESHEET COLOURS
        // Background:          #192231
        // Background Medium:   #222C3E
        // Background Light:    #273245
        // Text Dark:           #98878F
        // Text Light:          #B8A5AE
        // Red:                 #985E6D
        // Blue:                #494E6B
        // Medium Blue:         #626785
        // Light Blue:          #D0D2DB

        public MainWindow()
        {
            InitializeComponent();
            contentFrame.Navigate(new DefaultView());
            if(User.Email != "administrator@email.com")
            {
                btnCoordinator.Visibility = Visibility.Hidden;
            }
        }

        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void btnServiceRequest_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ServiceRequestView());
        }

        private void btnContractor_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ContractorView());
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new DefaultView());
        }

        private void btnCoordinator_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new CoordinatorView());
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ClientView());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            User.Email = String.Empty;
            User.Password = String.Empty;

            this.Hide();
            new LoginWindow().Show();
            this.Close();
        }
    }
}
