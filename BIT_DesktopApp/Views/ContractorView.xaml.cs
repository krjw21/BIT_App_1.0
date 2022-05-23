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
    /// Interaction logic for ContractorView.xaml
    /// </summary>
    public partial class ContractorView : Page
    {
        public ContractorView()
        {
            InitializeComponent();
            this.DataContext = new ContractorViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddContractorView();
            newWindow.ShowDialog();
        }
    }
}
