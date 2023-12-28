using Delivery_Service.Data;
using Delivery_Service.Services;
using Delivery_Service.Views.Pages.Admin;
using Delivery_Service.Views.Pages.Auth;
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

namespace Delivery_Service.Views.Windows {
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        IDataManager _dataManager;
        public AdminWindow(IDataManager dataManager) {
            _dataManager = dataManager;
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e) {
            AdminFrame.NavigationService.Navigate(new ListDishPage(_dataManager));

        }
        private void AdminFrame_ContentRendered(object sender, EventArgs e) {
        }
    }
}
