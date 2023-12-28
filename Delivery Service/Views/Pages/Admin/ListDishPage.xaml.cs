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
    /// Логика взаимодействия для ListDishPage.xaml
    /// </summary>
    public partial class ListDishPage : Page 
        {
        private readonly IDataManager _dataManager;
        public ListDishPage(IDataManager dataManager) {
            _dataManager = dataManager;
            InitializeComponent();
            DataContext = new ListDishViewModel(_dataManager);
            if (DataContext is ListDishViewModel listDishViewModel) {
                listDishViewModel.TryAddNewDish += NavigateToAddingDish;
            }
        }

        private void NavigateToAddingDish() {
            this.NavigationService.Navigate(new AddDishPage(_dataManager));
        }

        private void OrderList_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new ListOrderPage(_dataManager));
        }
    }
}

