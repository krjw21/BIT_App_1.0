﻿using BIT_DesktopApp.ViewModels;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddServiceRequestView();
            newWindow.ShowDialog();
        }
    }
}
