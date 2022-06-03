using BIT_DesktopApp.Models;
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

namespace BIT_DesktopApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            User.Email = txtEmail.Text;
            User.Password = txtPassword.Password;

            int userID;
            if (User.Email == "administrator@email.com")
            {
                userID = user.CheckUser();
                if (userID > 0)
                {
                    User.ID = userID;
                    this.Hide();
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect. Please try again.");
                }
            }
            else if (User.Email != "administrator@email.com")
            {
                userID = user.CheckUser();
                if (userID > 0)
                {
                    User.ID = userID;
                    this.Hide();
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect. Please try again.");
            }
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Clear();
        }
    }
}
