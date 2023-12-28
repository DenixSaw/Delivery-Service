using Delivery_Service.Data;
using Delivery_Service.ViewModels;
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

namespace Delivery_Service.Views.Pages.Admin {
    /// <summary>
    /// Логика взаимодействия для AddDishPage.xaml
    /// </summary>
    public partial class AddDishPage : Page {
        private IDataManager _dataManager;
        public AddDishPage(IDataManager dataManager) {
            _dataManager = dataManager;
            InitializeComponent();
            DataContext = new AddDishViewModel(_dataManager);

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new ListDishPage(_dataManager));
        }

        private void OrderList_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new ListOrderPage(_dataManager));

        }
    }
}
